using Application.Common.Models;

namespace Application.Common.Interfaces;

public interface IPaginatedQueryHandler<in TQuery, TQueryResult> : IQueryHandler<TQuery, PaginatedList<TQueryResult>>
    where TQuery : IPaginatedQuery<TQueryResult> { }
