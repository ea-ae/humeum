using Application.Common.Interfaces;

using Domain.Entities;

using MediatR;

namespace Application.Transactions.Commands.AddTransaction;

public record AddTransactionCommand : IRequest<int> {
    public int User { get; init; }
    public int Amount { get; init; }
}

public class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, int> {
    private readonly IAppDbContext _context;

    public AddTransactionCommandHandler(IAppDbContext context) => _context = context;

    public async Task<int> Handle(AddTransactionCommand request, CancellationToken token) {
        var transaction = new Transaction(request.Amount, TransactionType.Income, DateTime.UtcNow);

        _context.TransactionTypes.Attach(TransactionType.Income);
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(token);

        return transaction.Id;
    }
}
