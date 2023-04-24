using System.Linq.Expressions;

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Shared.Interfaces;
using Shared.Models;

namespace Application.Common.Extensions;

/// <summary>
/// Extensions for command and query handlers.
/// </summary>
internal static class ServiceExtensions {
    /// <summary>
    /// Asserts that a set of fields is either provided fully or not at all (aka every field is null).
    /// </summary>
    /// <param name="fields">Set of fields that must be either provided fully or not at all.</param>
    /// <returns>True if the fields were fully provided, false if none were provided. An error if they were partially provided.</returns>
    public static IResult<bool, ApplicationValidationException> AssertOptionalFieldSetValidity(this List<object?> fields) {
        int optionalFieldsProvided = fields.Count(field => field is not null);
        bool allFieldsProvided = optionalFieldsProvided == fields.Count;

        if (!allFieldsProvided && optionalFieldsProvided > 0) {
            var error = new ApplicationValidationException("Set of optional fields was only partially specified.");
            return Result<bool, ApplicationValidationException>.Fail(error);
        }

        return Result<bool, ApplicationValidationException>.Ok(allFieldsProvided);
    }

    /// <summary>
    /// Converts an ordered queryable of source entities to a paginated list of DTOs.
    /// </summary>
    /// <typeparam name="T">Type of items in paginated list.</typeparam>
    /// <param name="query">Queryable that contains all the ordered results.</param>
    /// <param name="request">Request object that contains offset/limit information.</param>
    /// <param name="mapper">Automapper that projects the source entities.</param>
    /// <returns>Paginated list.</returns>
    public static IResult<PaginatedList<TDestination>, IBaseException> ToPaginatedList<TSource, TDestination>(this IOrderedQueryable<TSource> query,
                                                                                                              IPaginatedQuery<TDestination> request,
                                                                                                              IMapper mapper) {
        return PaginatedList<TDestination>.ProjectAndCreateFromQuery(query, request, mapper);
    }

    /// <summary>
    /// If an entity is not found, returns a failed <see cref="NotFoundValidationException"/> result. 
    /// Otherwise, returns the entity in a result.
    /// </summary>
    /// <typeparam name="TSource">Type of the entity.</typeparam>
    /// <param name="mapper">Automapper.</param>
    /// <param name="query">Query with an entity.</param>
    /// <returns>A result that contains either the entity or a not found error.</returns>
    public static IResult<T, IBaseException> ToFoundResult<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate) {
        var entity = query.FirstOrDefault(predicate);

        if (entity is null) {
            return Result<T, IBaseException>.Fail(new NotFoundValidationException(typeof(T)));
        }

        return Result<T, IBaseException>.Ok(entity);
    }

    /// <summary>
    /// If the entity is null, returns a failed <see cref="NotFoundValidationException"/> result. 
    /// Otherwise, maps the entity to a DTO and returns a successful result.
    /// </summary>
    /// <typeparam name="TSource">Type of the source entity.</typeparam>
    /// <typeparam name="TDestination">Type of the destination mapped DTO.</typeparam>
    /// <param name="mapper">Automapper.</param>
    /// <param name="entity">Source entity.</param>
    /// <returns>A result that contains either the mapped DTO or a not found error.</returns>
    public static IResult<TDestination, IBaseException> ToMappedResultOrNotFound<TSource, TDestination>(this IMapper mapper, TSource? entity) {
        if (entity is null) {
            return Result<TDestination, IBaseException>.Fail(new NotFoundValidationException(typeof(TSource)));
        }

        return mapper.MapToResult<TDestination>(entity);
    }

    /// <summary>
    /// Maps an entity to a successful result containing a DTO.
    /// </summary>
    /// <typeparam name="T">Type to map entity to.</typeparam>
    /// <param name="mapper">Automapper.</param>
    /// <param name="entity">Source entity.</param>
    /// <returns>Mapped entity contained within a successful result.</returns>
    public static IResult<T, IBaseException> MapToResult<T>(this IMapper mapper, object entity) {
        T value = mapper.Map<T>(entity);
        return Result<T, IBaseException>.Ok(value);
    }

    /// <summary>
    /// Maps entities to a successful result containing a list of DTOs.
    /// </summary>
    /// <typeparam name="T">Type to map entities to.</typeparam>
    /// <param name="query">Queryable that contains all the source entities.</param>
    /// <param name="mapper">Automapper.</param>
    /// <returns>Projected entities contained within a successful result.</returns>
    public static IResult<List<T>, IBaseException> ProjectToResult<T>(this IQueryable query, IMapper mapper) {
        var dto = query.ProjectTo<T>(mapper.ConfigurationProvider).ToList();
        return Result<List<T>, IBaseException>.Ok(dto);
    }
}
