using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.Common.Interfaces;
using Domain.Common.Models;
using Domain.TransactionAggregate;

using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries;

public record GetTransactionQuery : IQuery<IResult<TransactionDto>> {
    [Required] public required int Profile { get; init; }
    [Required] public required int Transaction { get; init; }
}

public class GetTransactionQueryHandler : IQueryHandler<GetTransactionQuery, IResult<TransactionDto>> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetTransactionQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public Task<IResult<TransactionDto>> Handle(GetTransactionQuery request, CancellationToken token) {
        var transaction = _context.Transactions.AsNoTracking()
                                               .Include(t => t.Categories)
                                               .Include(t => t.Type)
                                               .Include(t => t.TaxScheme)
                                               .Include(t => t.Asset)
                                               .Include(t => t.PaymentTimeline.Frequency).ThenInclude(f => f!.TimeUnit)
                                               .FirstOrDefault(t => t.Id == request.Transaction
                                                                    && t.ProfileId == request.Profile
                                                                    && t.DeletedAt == null);

        if (transaction is null) {
            IResult<TransactionDto> failResult = Result<TransactionDto>.Fail(new NotFoundValidationException(typeof(Transaction)));
            return Task.FromResult(failResult);
        }


        var result = _mapper.MapToResult<TransactionDto>(transaction);
        return Task.FromResult(result);
    }
}
