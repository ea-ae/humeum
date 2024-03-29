﻿using System.Collections;

using Application.Common.Exceptions;
using Application.Common.Interfaces;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Shared.Interfaces;
using Shared.Models;

namespace Application.Common.Models;

/// <summary>
/// An offset-limit paginated list.
/// </summary>
/// <typeparam name="T"></typeparam>
public class PaginatedList<T> : IReadOnlyList<T> {
    const int MAX_LIMIT = 100;

    IReadOnlyList<T> _list;

    public T this[int index] => _list[index];

    public int Count => _list.Count;

    public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

    private PaginatedList(IReadOnlyList<T> list) {
        _list = list;
    }

    /// <summary>
    /// Create a paginated list from an entity query and request combination.
    /// </summary>
    /// <param name="query">Ordered queryable that contains unmapped entities.</param>
    /// <param name="request">Request record that contains the offset/limit fields.</param>
    /// <param name="mapper">Automapper that projects the entities to the required DTO type.</param>
    /// <returns>Paginated list of DTOs.</returns>
    public static IResult<PaginatedList<T>, ApplicationValidationException> ProjectAndCreateFromQuery<TSource>(IOrderedQueryable<TSource> query,
                                                                                                               IPaginatedQuery<T> request,
                                                                                                               IMapper mapper) {
        if (request.Offset > MAX_LIMIT) {
            return Result<PaginatedList<T>, ApplicationValidationException>.Fail(
                new ApplicationValidationException($"Limit cannot be higher than {MAX_LIMIT}."));
        }

        //var query2 = query.Skip(request.Offset);
        //query2 = query.Take(request.Limit);
        var x = query.ProjectTo<T>(mapper.ConfigurationProvider);
        var list = x.ToList();
        var paginatedList = new PaginatedList<T>(list);
        return Result<PaginatedList<T>, ApplicationValidationException>.Ok(paginatedList);
    }
}
