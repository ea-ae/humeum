using System.Collections;

using Application.Common.Interfaces;

namespace Application.Common.Models;

/// <summary>
/// An offset-limit paginated list.
/// </summary>
/// <typeparam name="T"></typeparam>
public class PaginatedList<T> : IReadOnlyList<T>
{
    IReadOnlyList<T> _list;

    public PaginatedList(IQueryable<T> query, IPaginatedQuery<T> request)
    {
        _list = query.Skip(request.Offset).Take(request.Limit).ToList();
    }

    public T this[int index] => _list[index];

    public int Count => _list.Count;

    public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();
}
