﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;

using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries;

public record GetUserTransactionQuery : IQuery<TransactionDto> {
    public int User { get; init; }
    public int Profile { get; init; }
    public int Transaction { get; init; }
}

public class GetuserTransactionQueryHandler : IQueryHandler<GetUserTransactionQuery, TransactionDto> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetuserTransactionQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TransactionDto> Handle(GetUserTransactionQuery request, CancellationToken _) {
        var transaction = _context.Transactions.Include(t => t.Profile)
                                               .FirstOrDefault(t => t.Id == request.Transaction
                                                                    && t.ProfileId == request.Profile
                                                                    && t.Profile.UserId == request.User
                                                                    && t.DeletedAt == null);

        if (transaction is null) {
            throw new NotFoundValidationException("Transaction does not exist.");
        }

        return await Task.Run(() => _mapper.Map<TransactionDto>(transaction));
    }
}

