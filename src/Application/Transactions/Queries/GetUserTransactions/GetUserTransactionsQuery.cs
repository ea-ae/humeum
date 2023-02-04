using Application.Common.Interfaces;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries.GetUserTransactions;

/// <summary>
/// Get transactions for a specified user with optional filtering conditions.
/// </summary>
public record GetUserTransactionsQuery : IQuery<List<TransactionDto>> {
    public int User { get; init; } // todo: unused for now

    public DateTime? StartBefore { get; init; } = null;
    public DateTime? StartAfter { get; init; } = null;
}

public class GetUserTransactionsQueryHandler : IQueryHandler<GetUserTransactionsQuery, List<TransactionDto>> {
    private readonly IAppDbContext _context;

    public GetUserTransactionsQueryHandler(IAppDbContext context) => _context = context;

    public async Task<List<TransactionDto>> Handle(GetUserTransactionsQuery request, CancellationToken token) {
        var transactions = _context.Transactions.Select(t => t);
        
        if (request.StartBefore is not null) {
            transactions = transactions.Where(t => t.PaymentStart < request.StartBefore);
        }
        if (request.StartAfter is not null) {
            transactions = transactions.Where(t => t.PaymentStart > request.StartAfter);
        }

        return await transactions.Select(t => new TransactionDto(t)).ToListAsync(token);
    }
}
