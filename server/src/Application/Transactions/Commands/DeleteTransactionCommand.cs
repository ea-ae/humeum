using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.Common.Interfaces;
using Domain.Common.Models;
using Domain.TransactionAggregate;

using MediatR;

namespace Application.Transactions.Commands;

public record DeleteTransactionCommand : ICommand<IResult<None>> {
    [Required] public required int Profile { get; init; }
    [Required] public required int Transaction { get; init; }
}

public class DeleteTransactionCommandHandler : ICommandHandler<DeleteTransactionCommand, IResult<None>> {
    private readonly IAppDbContext _context;

    public DeleteTransactionCommandHandler(IAppDbContext context) => _context = context;

    public async Task<IResult<None>> Handle(DeleteTransactionCommand request, CancellationToken token = default) {
        var transaction = _context.Transactions.FirstOrDefault(t => t.Id == request.Transaction
                                                                    && t.ProfileId == request.Profile
                                                                    && t.DeletedAt == null);

        if (transaction is null) {
            return Result<None>.Fail(new NotFoundValidationException(typeof(Transaction)));
        }

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync(token);

        return Result<None>.Ok(None.Value);
    }
}
