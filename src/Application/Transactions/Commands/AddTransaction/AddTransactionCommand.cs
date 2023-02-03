using Application.Common.Interfaces;

using Domain.Entities;

namespace Application.Transactions.Commands.AddTransaction;

public record AddTransactionCommand {
    public int Amount { get; init; }
}

public class AddTransactionCommandHandler {
    private readonly IAppDbContext _context;

    public AddTransactionCommandHandler(IAppDbContext context) => _context = context;

    public async Task<int> Handle(AddTransactionCommand command) {
        var transaction = new Transaction(command.Amount, TransactionType.Income, DateTime.UtcNow);

        _context.TransactionTypes.Attach(TransactionType.Income);
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return transaction.Id;
    }
}
