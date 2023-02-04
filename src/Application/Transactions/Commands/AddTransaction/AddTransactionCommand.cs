using Application.Common;
using Application.Common.Interfaces;

using Domain.Entities;

using MediatR;

namespace Application.Transactions.Commands.AddTransaction;

public record AddTransactionCommand : IRequest<int> {
    public int User { get; init; }
    public int Amount { get; init; }
    public required string Type { get; init; }
    public DateTime PaymentStart { get; init; }

    public DateTime? PaymentEnd { get; init; } = null;
    public string? TimeUnit { get; init; } = null;
    public int? TimesPerUnit { get; init; } = null;
}

public class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, int> {
    private readonly IAppDbContext _context;

    public AddTransactionCommandHandler(IAppDbContext context) => _context = context;

    public async Task<int> Handle(AddTransactionCommand request, CancellationToken token) {
        var transactionType = request.Type.ToEnumerationEntityByCode<TransactionType>(_context);

        Transaction transaction;
        if (request.PaymentEnd is not null && request.TimeUnit is not null && request.TimesPerUnit is not null) {
            var timeUnit = request.TimeUnit.ToEnumerationEntityByCode<TimeUnit>(_context);
            var frequency = new Frequency(timeUnit, (int)request.TimesPerUnit);
            transaction = new Transaction(request.Amount,
                                          transactionType,
                                          request.PaymentStart,
                                          (DateTime)request.PaymentEnd,
                                          frequency);
        } else {
            transaction = new Transaction(request.Amount, transactionType, request.PaymentStart);
        }

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(token);

        return transaction.Id;
    }
}
