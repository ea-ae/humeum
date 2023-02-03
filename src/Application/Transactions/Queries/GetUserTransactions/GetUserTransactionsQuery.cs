using Application.Common.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries.GetUserTransactions;

public class GetUserTransactionsQueryHandler {
    private readonly IAppDbContext _context;

    public GetUserTransactionsQueryHandler(IAppDbContext context) => _context = context;

    public async Task<List<TransactionDto>> Handle() {
        return await _context.Transactions
                             .Select(t => new TransactionDto(t))
                             .ToListAsync();
    }
}
