using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.Common.Interfaces;
using Domain.Common.Models;
using Domain.ProfileAggregate;
using Domain.TransactionCategoryAggregate;

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
    public static IResult<T, IBaseException> GetEnumerationEntityByCode<T>(this IAppDbContext context, string code) where T : Enumeration {
        T enumEntity;
        try {
            enumEntity = Enumeration.GetByCode<T>(code);
        } catch (InvalidOperationException) {
            return Result<T, IBaseException>.Fail(new ApplicationValidationException($"Incorrect enumeration code {code} for {typeof(T).Name}"));
        }

        context.Set<T>().Attach(enumEntity);
        return Result<T, IBaseException>.Ok(enumEntity);
    }

    // TODO: A simple entity Exists() extension method for validation with a pre-made exception
}
