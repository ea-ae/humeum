using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;

using Domain.TransactionAggregate;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Commands;

public record DeleteTransactionCommand : ICommand {
    [Required] public required int Profile { get; init; }
    [Required] public required int Transaction { get; init; }
}

public class DeleteTransactionCommandHandler : ICommandHandler<DeleteTransactionCommand> {
    private readonly IAppDbContext _context;

    public DeleteTransactionCommandHandler(IAppDbContext context) => _context = context;

    public async Task<Unit> Handle(DeleteTransactionCommand request, CancellationToken token = default) {
        var transaction = _context.Transactions.FirstOrDefault(t => t.Id == request.Transaction
                                                                    && t.ProfileId == request.Profile
                                                                    && t.DeletedAt == null);

        if (transaction is null) {
            throw new NotFoundValidationException(typeof(Transaction));
        }

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync(token);

        return Unit.Value;
    }
}
