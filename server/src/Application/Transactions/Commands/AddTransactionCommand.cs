using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;

using Domain.ProfileAggregate;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;

namespace Application.Transactions.Commands.AddTransaction;

public record AddTransactionCommand : ICommand<int> {
    [Required] public required int User { get; init; }
    [Required] public required int Profile { get; init; }

    public string? Name { get; init; }
    public string? Description { get; init; }
    [Required] public decimal? Amount { get; init; }
    [Required] public required string Type { get; init; }
    [Required] public DateOnly? PaymentStart { get; init; }
    [Required] public required int TaxScheme { get; init; }
    public int? Asset { get; init; }

    public DateOnly? PaymentEnd { get; init; }
    public string? TimeUnit { get; init; }
    public int? TimesPerCycle { get; init; }
    public int? UnitsInCycle { get; init; }
}

public class AddTransactionCommandHandler : ICommandHandler<AddTransactionCommand, int> {
    private readonly IAppDbContext _context;

    public AddTransactionCommandHandler(IAppDbContext context) => _context = context;

    public async Task<int> Handle(AddTransactionCommand request, CancellationToken token = default) {
        // validation

        List<object?> recurringTransactionFields = new() { // fields required for recurrent transactions
            request.PaymentEnd,
            request.TimeUnit,
            request.TimesPerCycle,
            request.UnitsInCycle
        };

        int recurringTransactionFieldCount = recurringTransactionFields.Count(field => field is not null);
        bool isRecurringTransaction = recurringTransactionFieldCount == recurringTransactionFields.Count;

        if (!isRecurringTransaction && recurringTransactionFieldCount > 0) {
            throw new ApplicationValidationException("Fields for recurrent transactions were only partially specified.");
        }

        _context.AssertUserOwnsProfile(request.User, request.Profile);

        // handling

        var transactionType = _context.GetEnumerationEntityByCode<TransactionType>(request.Type);

        Transaction transaction;
        if (isRecurringTransaction) {
            var timeUnit = _context.GetEnumerationEntityByCode<TimeUnit>(request.TimeUnit!);
            var paymentPeriod = new TimePeriod((DateOnly)request.PaymentStart!, (DateOnly)request.PaymentEnd!);
            var paymentFrequency = new Frequency(timeUnit, (int)request.TimesPerCycle!, (int)request.UnitsInCycle!);
            var paymentTimeline = new Timeline(paymentPeriod, paymentFrequency);
            transaction = new Transaction(request.Name, request.Description, (decimal)request.Amount!, transactionType, 
                                          paymentTimeline, request.Profile!, request.TaxScheme);
        } else {
            var timeline = new Timeline(new TimePeriod((DateOnly)request.PaymentStart!));
            transaction = new Transaction(request.Name, request.Description, (decimal)request.Amount!, transactionType, 
                                          timeline, request.Profile!, request.TaxScheme);
        }

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(token);

        return transaction.Id;
    }
}
