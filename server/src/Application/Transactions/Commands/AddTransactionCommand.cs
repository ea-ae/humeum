using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;

using Domain.ProfileAggregate;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;

namespace Application.Transactions.Commands.AddTransaction;

/// <summary>
/// Create a new transaction for a user profile. Transactions can either be single-payment, in which
/// case the optional recurring transaction fields are not provided; or they can be recurrent,
/// meaning they'll be performed at a certain frequency up until the payment end date.
/// The first payment is always made at the payment start date.
/// </summary>
public record AddTransactionCommand : ICommand<int> {
    public required int User { get; init; }
    public required int Profile { get; init; }

    public string? Name { get; init; }
    public string? Description { get; init; }
    [Required] public decimal? Amount { get; init; }
    public required string Type { get; init; }
    [Required] public DateOnly? PaymentStart { get; init; }

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
            throw new Common.Exceptions.ValidationException(
                "Fields for recurrent transactions were only partially specified.");
        }

        // TODO: we handle this differently in different services!!
        bool userOwnsProfile = _context.Profiles.Any(p => p.Id == request.Profile && p.UserId == request.User);
        if (!userOwnsProfile) {
            throw new NotFoundValidationException(typeof(Profile));
        }

        // handling

        var transactionType = _context.GetEnumerationEntityByCode<TransactionType>(request.Type);

        Transaction transaction;
        if (isRecurringTransaction) {
            var timeUnit = _context.GetEnumerationEntityByCode<TimeUnit>(request.TimeUnit!);
            var paymentPeriod = new TimePeriod((DateOnly)request.PaymentStart!, (DateOnly)request.PaymentEnd!);
            var paymentFrequency = new Frequency(timeUnit, (int)request.TimesPerCycle!, (int)request.UnitsInCycle!);
            var paymentTimeline = new Timeline(paymentPeriod, paymentFrequency);
            transaction = new Transaction(request.Profile!, request.Name, request.Description,
                                          (decimal)request.Amount!, transactionType, paymentTimeline);
        } else {
            var timeline = new Timeline(new TimePeriod((DateOnly)request.PaymentStart!));
            transaction = new Transaction(request.Profile!, request.Name, request.Description,
                                          (decimal)request.Amount!, transactionType, timeline);
        }

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(token);

        return transaction.Id;
    }
}
