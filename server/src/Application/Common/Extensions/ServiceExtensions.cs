using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Common.Extensions;

/// <summary>
/// Extensions for command and query handlers.
/// </summary>
internal static class ServiceExtensions {
    /// <summary>
    /// Asserts that a set of fields is either provided fully or not at all (aka every field is null).
    /// </summary>
    /// <param name="fields">Set of fields that must be either provided fully or not at all.</param>
    /// <returns>True if the fields were fully provided, false if none were provided.</returns>
    /// <exception cref="ApplicationValidationException">If the fields were provided partially and the assertion failed.</exception>
    public static bool AssertOptionalFieldSetValidity(this List<object?> fields) {
        int optionalFieldsProvided = fields.Count(field => field is not null);
        bool allFieldsProvided = optionalFieldsProvided == fields.Count;

        if (!allFieldsProvided && optionalFieldsProvided > 0) {
            throw new ApplicationValidationException("Set of optional fields was only partially specified.");
        }

        return allFieldsProvided;
    }

    /// <summary>
    /// Converts an IQueryable to a paginated list. Be sure to order the results (e.g. by ascending id) first.
    /// </summary>
    /// <typeparam name="T">Type of items in paginated list.</typeparam>
    /// <param name="query">IQueryable that contains all the ordered resluts.</param>
    /// <param name="request">Request object that contains offset/limit information.</param>
    /// <returns>Paginated list.</returns>
    public static PaginatedList<T> ToPaginatedList<T>(this IQueryable<T> query, IPaginatedQuery<T> request) {
        return new PaginatedList<T>(query, request);
    }
}
