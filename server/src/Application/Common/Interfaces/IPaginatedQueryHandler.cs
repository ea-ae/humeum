using Application.Common.Models;

using Domain.Common.Interfaces;

namespace Application.Common.Interfaces;

public interface IPaginatedQueryHandler<in TQuery, TQueryResult> : IQueryHandler<TQuery, IResult<PaginatedList<TQueryResult>>>
    where TQuery : IPaginatedQuery<TQueryResult> { }
