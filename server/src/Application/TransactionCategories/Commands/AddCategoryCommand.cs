using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;

using Domain.TransactionCategoryAggregate;

namespace Application.TransactionCategories.Commands.AddCategory;

public record AddCategoryCommand : ICommand<int>
{
    [Required] public required int User { get; init; }
    [Required] public required int Profile { get; init; }

    [Required] public required string Name { get; init; }
}

public class AddCategoryCommandHandler : ICommandHandler<AddCategoryCommand, int>
{
    private readonly IAppDbContext _context;

    public AddCategoryCommandHandler(IAppDbContext context) => _context = context;

    public async Task<int> Handle(AddCategoryCommand request, CancellationToken token = default) {
        // validation

        _context.AssertUserOwnsProfile(request.User, request.Profile);

        // handling

        var category = new TransactionCategory(request.Name, request.Profile);
        _context.TransactionCategories.Add(category);
        await _context.SaveChangesAsync();

        return category.Id;
    }
}
