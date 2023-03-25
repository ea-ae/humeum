using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Commands;

public record RemoveTransactionCategoryCommand : ICommand {
    [Required] public required int Profile { get; init; }
    [Required] public required int Transaction { get; init; }

    [Required] public required int? Category { get; init; }
}

public class RemoveTransactionCategoryCommandHandler : ICommandHandler<RemoveTransactionCategoryCommand> {
    private readonly IAppDbContext _context;

    public RemoveTransactionCategoryCommandHandler(IAppDbContext context) => _context = context;

    public async Task<Unit> Handle(RemoveTransactionCategoryCommand request, CancellationToken token = default) {
        // validation

        var transaction = _context.Transactions.Include(t => t.Categories).FirstOrDefault(t => t.Id == request.Transaction
                                                                                               && t.ProfileId == request.Profile
                                                                                               && t.DeletedAt == null);
        if (transaction is null) {
            throw new NotFoundValidationException("Transaction with given ID was not found for profile.");
        }

        var category = _context.TransactionCategories.FirstOrDefault(tc => tc.Id == request.Category
                                                                           && (tc.ProfileId == null || tc.ProfileId == request.Profile)
                                                                           && tc.DeletedAt == null);
        if (category is null) {
            throw new NotFoundValidationException("Transaction category with given ID was not found for profile.");
        }

        // handling

        bool categoryRemoved = transaction.RemoveCategory(category);

        if (!categoryRemoved) {
            throw new NotFoundValidationException("Specified category was not found on the transaction.");
        }

        await _context.SaveChangesAsync(token);
        return Unit.Value;
    }
}
