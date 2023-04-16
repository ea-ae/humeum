using Application.Common.Models;

using Shared.Interfaces;

namespace Application.Common.Interfaces;

public interface IPaginatedQueryHandler<in TQuery, TQueryResult> : IQueryHandler<TQuery, IResult<PaginatedList<TQueryResult>, IBaseException>>
    where TQuery : IPaginatedQuery<TQueryResult> { }
