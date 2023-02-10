using Application.Common.Interfaces;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries.GetUserTransactions;

/// <summary>
/// Get transactions for a specified user with optional filtering conditions.
/// </summary>
public record GetUserTransactionsQuery : IQuery<List<TransactionDto>> {
    public required int User { get; init; } // todo: unused for now

    public DateOnly? StartBefore { get; init; }
    public DateOnly? StartAfter { get; init; }
}

public class GetUserTransactionsQueryHandler : IQueryHandler<GetUserTransactionsQuery, List<TransactionDto>> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetUserTransactionsQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TransactionDto>> Handle(GetUserTransactionsQuery request, CancellationToken token) {
        var transactions = _context.Transactions.AsNoTracking().Where(t => t.DeletedAt == null).Select(t => t);

        if (request.StartBefore is not null) {
            transactions = transactions.Where(t => t.PaymentTimeline.Period.Start < request.StartBefore);
        }
        if (request.StartAfter is not null) {
            transactions = transactions.Where(t => t.PaymentTimeline.Period.Start > request.StartAfter);
        }

        return await transactions.ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
                                 .ToListAsync(token);
    }
}
