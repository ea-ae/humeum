using System.ComponentModel.DataAnnotations;

using Application.Common.Extensions;
using Application.Common.Interfaces;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries.GetTransactions;

public record GetTransactionsQuery : IQuery<List<TransactionDto>> {
    [Required] public required int User { get; init; }
    [Required] public required int Profile { get; init; }

    public DateOnly? StartBefore { get; init; }
    public DateOnly? StartAfter { get; init; }
}

public class GetTransactionsQueryHandler : IQueryHandler<GetTransactionsQuery, List<TransactionDto>> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetTransactionsQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken token = default) {
        var transactions = _context.Transactions.AsNoTracking()
                                                .Include(t => t.Categories)
                                                .Where(t => t.ProfileId == request.Profile
                                                            && t.DeletedAt == null);

        if (request.StartBefore is not null) {
            transactions = transactions.Where(t => t.PaymentTimeline.Period.Start <= request.StartBefore);
        }
        if (request.StartAfter is not null) {
            transactions = transactions.Where(t => t.PaymentTimeline.Period.Start >= request.StartAfter);
        }

        return Task.Run(() => transactions.ProjectTo<TransactionDto>(_mapper.ConfigurationProvider).ToList());
    }
}
