using System.ComponentModel.DataAnnotations;

using Application.Common.Interfaces;

using Domain.TransactionCategoryAggregate;

namespace Application.TransactionCategories.Commands;

public record AddCategoryCommand : ICommand<int> {
    [Required] public required int Profile { get; init; }

    [Required] public required string Name { get; init; }
}

public class AddCategoryCommandHandler : ICommandHandler<AddCategoryCommand, int> {
    private readonly IAppDbContext _context;

    public AddCategoryCommandHandler(IAppDbContext context) => _context = context;

    public async Task<int> Handle(AddCategoryCommand request, CancellationToken token = default) {
        var category = new TransactionCategory(request.Name, request.Profile);

        _context.TransactionCategories.Add(category);
        await _context.SaveChangesAsync(token);

        return category.Id;
    }
}
