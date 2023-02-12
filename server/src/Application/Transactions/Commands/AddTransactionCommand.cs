using System.ComponentModel.DataAnnotations;

using Application.Common.Extensions;
using Application.Common.Interfaces;

using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;

namespace Application.Transactions.Commands.AddTransaction;

/// <summary>
/// Create a new transaction for a user. Transactions can either be single-payment, in which
/// case the optional recurring transaction fields are not provided; or they can be recurrent,
/// meaning they'll be performed at a certain frequency up until the payment end date.
/// The first payment is always made at the payment start date.
/// </summary>
public record AddTransactionCommand : ICommand<int> {
    [Required] public int? User { get; init; }
    [Required] public int? Amount { get; init; }
    public required string Type { get; init; }
    [Required] public DateOnly? PaymentStart { get; init; }

    public DateOnly? PaymentEnd { get; init; }
    public string? TimeUnit { get; init; }
    public int? TimesPerUnit { get; init; }
}

public class AddTransactionCommandHandler : ICommandHandler<AddTransactionCommand, int> {
    private readonly IAppDbContext _context;

    public AddTransactionCommandHandler(IAppDbContext context) => _context = context;

    public async Task<int> Handle(AddTransactionCommand request, CancellationToken token) {
        // validation

        List<object?> recurringTransactionFields = new() { // fields required for recurrent transactions
            request.PaymentEnd,
            request.TimeUnit,
            request.TimesPerUnit
        };

        int recurringTransactionFieldCount = recurringTransactionFields.Count(field => field is not null);
        bool isRecurringTransaction = recurringTransactionFieldCount == recurringTransactionFields.Count;

        if (!isRecurringTransaction && recurringTransactionFieldCount > 0) {
            throw new Common.Exceptions.ValidationException(
                "Fields for recurrent transactions were only partially specified.");
        }

        // handling

        var transactionType = _context.GetEnumerationEntityByCode<TransactionType>(request.Type);

        Transaction transaction;
        if (isRecurringTransaction) {
            var timeUnit = _context.GetEnumerationEntityByCode<TimeUnit>(request.TimeUnit!);
            var paymentPeriod = new TimePeriod((DateOnly)request.PaymentStart!, (DateOnly)request.PaymentEnd!);
            var paymentFrequency = new Frequency(timeUnit, (int)request.TimesPerUnit!);
            var paymentTimeline = new Timeline(paymentPeriod, paymentFrequency);
            transaction = new Transaction((decimal)request.Amount!, transactionType, paymentTimeline);
        } else {
            var timeline = new Timeline(new TimePeriod((DateOnly)request.PaymentStart!));
            transaction = new Transaction((decimal)request.Amount!, transactionType, timeline);
        }

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(token);

        return transaction.Id;
    }
}
