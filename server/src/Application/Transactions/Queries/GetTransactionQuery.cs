using System.ComponentModel.DataAnnotations;

using Application.Common.Extensions;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.TransactionAggregate;

using Microsoft.EntityFrameworkCore;

using Shared.Interfaces;

namespace Application.Transactions.Queries;

public record GetTransactionQuery : IQuery<IResult<TransactionDto, IBaseException>> {
    [Required] public required int Profile { get; init; }
    [Required] public required int Transaction { get; init; }
}

public class GetTransactionQueryHandler : IQueryHandler<GetTransactionQuery, IResult<TransactionDto, IBaseException>> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetTransactionQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public Task<IResult<TransactionDto, IBaseException>> Handle(GetTransactionQuery request, CancellationToken token) {
        var transaction = _context.Transactions.AsNoTracking()
                                               .Include(t => t.Categories)
                                               .Include(t => t.Type)
                                               .Include(t => t.TaxScheme)
                                               .Include(t => t.Asset)
                                               .Include(t => t.PaymentTimeline.Frequency).ThenInclude(f => f!.TimeUnit)
                                               .FirstOrDefault(t => t.Id == request.Transaction
                                                                    && t.ProfileId == request.Profile
                                                                    && t.DeletedAt == null);

        var result = _mapper.ToMappedResultOrNotFound<Transaction, TransactionDto>(transaction);
        return Task.FromResult(result);
    }
}
