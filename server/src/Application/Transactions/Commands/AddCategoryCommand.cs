using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;

using Domain.TransactionCategoryAggregate;

namespace Application.Transactions.Commands;

public record AddCategoryCommand : ICommand<int> {
    [Required] public required int User { get; init; }
    [Required] public required int Profile { get; init; }
    [Required] public required int Transaction { get; init; }

    [Required] public required string Name { get; init; }
}

public class AddCategoryCommandHandler : ICommandHandler<AddCategoryCommand, int> {
    private readonly IAppDbContext _context;

    public AddCategoryCommandHandler(IAppDbContext context) => _context = context;

    public async Task<int> Handle(AddCategoryCommand request, CancellationToken token = default) {
        // validation

        _context.AssertUserOwnsProfile(request.User, request.Profile);

        var transactionExists = _context.Transactions.Any(t => t.Id == request.Transaction && t.DeletedAt == null);
        if (!transactionExists) {
            throw new NotFoundValidationException("Transaction with given ID was not found.");
        }

        // handling

        var category = new TransactionCategory(request.Name, request.Profile);
        _context.TransactionCategories.Add(category);
        await _context.SaveChangesAsync();

        return category.Id;
    }
}
