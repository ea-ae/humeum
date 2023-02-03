using Application.Common.Interfaces;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries.GetUserTransactions;

public record GetUserTransactionsQuery : IRequest<List<TransactionDto>> {
    public int User { get; init; } // todo: unused for now
}

public class GetUserTransactionsQueryHandler : IRequestHandler<GetUserTransactionsQuery, List<TransactionDto>> {
    private readonly IAppDbContext _context;

    public GetUserTransactionsQueryHandler(IAppDbContext context) => _context = context;

    public async Task<List<TransactionDto>> Handle(GetUserTransactionsQuery request, CancellationToken _) {
        return await _context.Transactions
                             .Select(t => new TransactionDto(t))
                             .ToListAsync();
    }
}
