using Application.Common;
using Application.Common.Interfaces;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;

namespace Application.Transactions.Commands.AddTransaction;

/// <summary>
/// Create a new transaction for a user. Transactions can either be single-payment, in which
/// case the optional RecurringTransactionFields are not provided; or they can be recurrent,
/// meaning they'll be performed at a certain frequency up until the payment end date.
/// The first payment is always made at the payment start date.
/// </summary>
public record AddTransactionCommand : ICommand<int> {
    public required int User { get; init; }
    public required int Amount { get; init; }
    public required string Type { get; init; }
    public required DateTime PaymentStart { get; init; }

    public RecurringTransactionFields? Recurring { get; init; }
}

/// <summary>
/// Optional fields that are specified for transactions with recurrent payments
/// within a specified time period and a frequency at which the payments are made.
/// </summary>
public record RecurringTransactionFields {
    public required DateTime PaymentEnd { get; init; }
    public required string TimeUnit { get; init; }
    public required int TimesPerUnit { get; init; }
}

public class AddTransactionCommandHandler : ICommandHandler<AddTransactionCommand, int> {
    private readonly IAppDbContext _context;

    public AddTransactionCommandHandler(IAppDbContext context) => _context = context;

    public async Task<int> Handle(AddTransactionCommand request, CancellationToken token) {
        var transactionType = _context.GetEnumerationEntityByCode<TransactionType>(request.Type);

        Transaction transaction;
        if (request.Recurring is not null) {
            var timeUnit = _context.GetEnumerationEntityByCode<TimeUnit>(request.Recurring.TimeUnit);
            var paymentPeriod = new TimePeriod(request.PaymentStart, request.Recurring.PaymentEnd);
            var paymentFrequency = new Frequency(timeUnit, request.Recurring.TimesPerUnit);
            var paymentTimeline = new Timeline(paymentPeriod, paymentFrequency);
            transaction = new Transaction(request.Amount, transactionType, paymentTimeline);
        } else {
            var timeline = new Timeline(new TimePeriod((DateTime)request.PaymentStart!));
            transaction = new Transaction(request.Amount, transactionType, timeline);
        }

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(token);

        return transaction.Id;
    }
}
