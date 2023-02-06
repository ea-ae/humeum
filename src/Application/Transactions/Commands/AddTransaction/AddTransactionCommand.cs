using Application.Common;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;

namespace Application.Transactions.Commands.AddTransaction;

/// <summary>
/// Create a new transaction for a user. Transactions can either be single-payment,
/// in which case they do not contain any of the nullable fields such as the payment
/// end date; or they can be recurrent, meaning they'll be performed at a certain frequency
/// up until the payment end date. The first payment is always made at the payment start date.
/// </summary>
public record AddTransactionCommand : ICommand<int> {
    public int User { get; init; }
    public int Amount { get; init; }
    public required string Type { get; init; }
    public DateTime PaymentStart { get; init; }

    public DateTime? PaymentEnd { get; init; }
    public string? TimeUnit { get; init; }
    public int? TimesPerUnit { get; init; }
}

public class AddTransactionCommandHandler : ICommandHandler<AddTransactionCommand, int> {
    private readonly IAppDbContext _context;

    public AddTransactionCommandHandler(IAppDbContext context) => _context = context;

    public async Task<int> Handle(AddTransactionCommand request, CancellationToken token) {
        // validation

        List<object?> recurrentTransactionFields = new() { // fields required for recurrent transactions
            request.PaymentEnd,
            request.TimeUnit,
            request.TimesPerUnit
        };
        bool recurrentTransaction = recurrentTransactionFields.All(field => field is not null);
        if (!recurrentTransaction && recurrentTransactionFields.Any(field => field is null)) {
            // todo refer to object that failed
            throw new ValidationException("Fields for recurrent transactions were only partially specified.");
        }

        // handling

        var transactionType = _context.GetEnumerationEntityByCode<TransactionType>(request.Type);

        Transaction transaction;
        if (recurrentTransaction) {
            var timeUnit = _context.GetEnumerationEntityByCode<TimeUnit>(request.TimeUnit!);
            var paymentPeriod = new TimePeriod(request.PaymentStart, (DateTime)request.PaymentEnd!);
            var paymentFrequency = new Frequency(timeUnit, (int)request.TimesPerUnit!);
            var timeline = new Timeline(paymentPeriod, paymentFrequency);
            transaction = new Transaction(request.Amount, transactionType, timeline);
        } else {
            var timeline = new Timeline(new TimePeriod(request.PaymentStart));
            transaction = new Transaction(request.Amount, transactionType, timeline);
        }

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(token);

        return transaction.Id;
    }
}
