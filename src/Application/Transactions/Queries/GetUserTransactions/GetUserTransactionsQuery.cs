using Application.Common.Interfaces;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries.GetUserTransactions;

/// <summary>
/// Get transactions for a specified user with optional filter conditions.
/// </summary>
public record GetUserTransactionsQuery : IQuery<List<TransactionDto>> {
    public int User { get; init; } // todo: unused for now
}

public class GetUserTransactionsQueryHandler : IQueryHandler<GetUserTransactionsQuery, List<TransactionDto>> {
    private readonly IAppDbContext _context;

    public GetUserTransactionsQueryHandler(IAppDbContext context) => _context = context;

    public async Task<List<TransactionDto>> Handle(GetUserTransactionsQuery request, CancellationToken _) {
        return await _context.Transactions
                             .Select(t => new TransactionDto(t))
                             .ToListAsync();
    }
}
