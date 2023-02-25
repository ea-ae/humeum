using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.Common;
using Domain.ProfileAggregate;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Extensions;

public static class AppDbContextExtensions {
    /// <summary>
    /// Returns an enumeration entity by code in an attached-to-database state.
    /// </summary>
    /// <typeparam name="T">Enumeration class type.</typeparam>
    /// <param name="context">Database context to attach entity to.</param>
    /// <param name="code">Enumeration entity code to match.</param>
    /// <returns>Attached enumeration entity that matches the given code.</returns>
    /// <exception cref="ApplicationValidationException">Thrown when the given code does not match any enumeration entity.</exception>
    public static T GetEnumerationEntityByCode<T>(this IAppDbContext context, string code) where T : Enumeration {
        T enumEntity;
        try {
            enumEntity = Enumeration.GetByCode<T>(code);
        } catch (InvalidOperationException) {
            throw new ApplicationValidationException($"Incorrect enumeration code {code} for {typeof(T).Name}");
        }

        context.Set<T>().Attach(enumEntity);
        return enumEntity;
    }
    
    /// <summary>
    /// Finds out whether a profile is owned by given user. Simplicity of method is paid for through an
    /// additional light SQL query.. This can be a useful check for entities that fall under a profile 
    /// scenarios where one must distinguish between no results returned (an empty list) due to lack of 
    /// data and due to an invalid profile condition.
    /// </summary>
    /// <param name="context">Database context to perform query on.</param>
    /// <param name="userId">The user to check profile ownership with.</param>
    /// <param name="profileId">The profile to check ownership of.</param>
    /// <exception cref="NotFoundValidationException">Thrown when the ownership assertion fails.</exception>
    public static void AssertUserOwnsProfile(this IAppDbContext context, int userId, int profileId) {
        bool userOwnsProfile = context.Profiles.Any(p => p.Id == profileId && p.UserId == userId && p.DeletedAt == null);
        if (!userOwnsProfile) {
            throw new NotFoundValidationException(typeof(Profile));
        }
    }

    // TODO: A simple entity Exists() extension method for validation with a pre-made exception
}
