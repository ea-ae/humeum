using System.ComponentModel.DataAnnotations;

using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Shared.Interfaces;
using Shared.Models;

namespace Application.Transactions.Queries;

public record GetTransactionsQuery : IPaginatedQuery<TransactionDto> {
    [Required] public required int Profile { get; init; }

    public DateOnly? StartBefore { get; init; }
    public DateOnly? StartAfter { get; init; }

    public int Offset { get; init; } = 0;
    public int Limit { get; init; } = 10;
}

public class GetTransactionsQueryHandler : IPaginatedQueryHandler<GetTransactionsQuery, TransactionDto> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetTransactionsQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public Task<IResult<PaginatedList<TransactionDto>, IBaseException>> Handle(GetTransactionsQuery request, CancellationToken token = default) {
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

        var list = transactions.OrderBy(t => t.Id).ToPaginatedList(request, _mapper);
        IResult<PaginatedList<TransactionDto>, IBaseException> result = Result<PaginatedList<TransactionDto>, IBaseException>.Ok(list);
        return Task.FromResult(result);
    }
}
