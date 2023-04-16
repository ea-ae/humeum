using Application.Common.Models;

using Domain.Common.Interfaces;

namespace Application.Common.Interfaces;

public interface IPaginatedQuery<TQueryResult> : IQuery<IResult<PaginatedList<TQueryResult>, IBaseException>> {
    public int Offset { get; init; }
    public int Limit { get; init; }
}
