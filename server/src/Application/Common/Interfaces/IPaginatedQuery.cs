using Application.Common.Models;

namespace Application.Common.Interfaces;

public interface IPaginatedQuery<TQueryResult> : IQuery<PaginatedList<TQueryResult>> {
    public int Offset { get; init; }
    public int Limit { get; init; }
}
