using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;

using Domain.ProfileAggregate;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Commands.AddTransaction;

public record AddCategoryToTransactionCommand : ICommand {
    [Required] public required int User { get; init; }
    [Required] public required int Profile { get; init; }
    [Required] public required int Transaction { get; init; }
    
    [Required] public required int? Category { get; init; }
}

public class AddCategoryToTransactionCommandHandler : ICommandHandler<AddCategoryToTransactionCommand> {
    private readonly IAppDbContext _context;

    public AddCategoryToTransactionCommandHandler(IAppDbContext context) => _context = context;

    public async Task<Unit> Handle(AddCategoryToTransactionCommand request, CancellationToken token = default) {
        // validation

        _context.AssertUserOwnsProfile(request.User, request.Profile);

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

        bool categoryAdded = transaction.AddCategory(category); // if category was already assigned, we won't assign it again

        if (!categoryAdded) { // POST isn't idempotent, throw conflict error as many-to-many link already exists
            throw new ConflictValidationException("Category is already assigned.");
        }

        await _context.SaveChangesAsync(token);
        return Unit.Value;
    }
}
