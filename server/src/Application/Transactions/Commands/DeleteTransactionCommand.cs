using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;

using Domain.TransactionAggregate;

using Shared.Interfaces;
using Shared.Models;

namespace Application.Transactions.Commands;

public record DeleteTransactionCommand : ICommand<IResult<None, IBaseException>> {
    [Required] public required int Profile { get; init; }
    [Required] public required int Transaction { get; init; }
}

public class DeleteTransactionCommandHandler : ICommandHandler<DeleteTransactionCommand, IResult<None, IBaseException>> {
    private readonly IAppDbContext _context;

    public DeleteTransactionCommandHandler(IAppDbContext context) => _context = context;

    public async Task<IResult<None, IBaseException>> Handle(DeleteTransactionCommand request, CancellationToken token = default) {
        var transactionResult = _context.Transactions.ToFoundResult(t => t.Id == request.Transaction
                                                                         && t.ProfileId == request.Profile
                                                                         && t.DeletedAt == null);

        return await transactionResult.ThenAsync<None, IBaseException>(async transaction => {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync(token);
            return Result<None, IBaseException>.Ok(None.Value);
        });
    }
}
