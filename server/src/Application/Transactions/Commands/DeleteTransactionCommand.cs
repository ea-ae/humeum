﻿using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;

using Domain.TransactionAggregate;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Commands.DeleteTransaction;

public record DeleteTransactionCommand : ICommand {
    [Required] public required int User { get; init; }
    [Required] public required int Profile { get; init; }
    [Required] public required int Transaction { get; init; }
}

public class DeleteTransactionCommandHandler : ICommandHandler<DeleteTransactionCommand> {
    private readonly IAppDbContext _context;

    public DeleteTransactionCommandHandler(IAppDbContext context) => _context = context;

    public async Task<Unit> Handle(DeleteTransactionCommand request, CancellationToken token = default) {
        var transaction = _context.Transactions.Include(t => t.Profile)
                                               .Where(t => t.Id == request.Transaction
                                                           && t.ProfileId == request.Profile
                                                           && t.Profile.UserId == request.User
                                                           && t.DeletedAt == null)
                                               .FirstOrDefault();

        if (transaction is null) {
            _context.AssertUserOwnsProfile(request.User, request.Profile);
            throw new NotFoundValidationException(typeof(Transaction));
        }

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync(token);

        return Unit.Value;
    }
}