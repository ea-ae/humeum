using Application.Common.Interfaces;

using Domain.Entities;

using MediatR;

namespace Application.Transactions.Commands.AddTransaction;

public record AddTransactionCommand : IRequest<int> {
    public int User { get; init; }
    public int Amount { get; init; }
    public required string TypeCode { get; init; }
    public DateTime PaymentStart { get; init; }

    //public DateTime? PaymentEnd { get; init; } = null;
    //public Frequency? Frequency { get; init; } = null;
}

public class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, int> {
    private readonly IAppDbContext _context;

    public AddTransactionCommandHandler(IAppDbContext context) => _context = context;

    public async Task<int> Handle(AddTransactionCommand request, CancellationToken token) {
        var transactionType = Domain.Common.EnumerationEntity.GetByCode<TransactionType>(request.TypeCode);
        var transaction = new Transaction(request.Amount,
                                          transactionType,
                                          request.PaymentStart);

        _context.TransactionTypes.Attach(transactionType);
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(token);

        return transaction.Id;
    }
}
