﻿using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.TransactionCategories.Commands.AddCategory;

using Domain.TransactionCategoryAggregate;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.TransactionCategories.Commands.DeleteCategory;

public record DeleteCategoryCommand : ICommand {
    [Required] public required int User { get; init; }
    [Required] public required int Profile { get; init; }
    [Required] public required int Category { get; init; }
}

public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand> {
    private readonly IAppDbContext _context;

    public DeleteCategoryCommandHandler(IAppDbContext context) => _context = context;

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken token = default) {
        var category = _context.TransactionCategories.Include(tc => tc.Profile)
                                                     .Where(tc => tc.Id == request.Category
                                                                  && tc.Profile != null
                                                                  && tc.ProfileId == request.Profile
                                                                  && tc.Profile.UserId == request.User
                                                                  && tc.Profile.DeletedAt == null)
                                                     .FirstOrDefault();

        if (category is null) {
            _context.AssertUserOwnsProfile(request.User, request.Profile);
            throw new NotFoundValidationException(typeof(TransactionCategory));
        }

        _context.TransactionCategories.Remove(category);
        await _context.SaveChangesAsync(token);

        return Unit.Value;
    }
}