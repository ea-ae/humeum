using System.ComponentModel.DataAnnotations;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

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

    public Task<PaginatedList<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken token = default) {
        if (request.Limit > 100) {
            throw new ValidationException("Limit cannot be higher than 100.");
        }

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

        return Task.Run(() => transactions.OrderBy(t => t.Id).ProjectTo<TransactionDto>(_mapper.ConfigurationProvider).ToPaginatedList(request));
    }
}
