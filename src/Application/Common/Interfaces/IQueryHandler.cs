using MediatR;

namespace Application.Common.Interfaces;

public interface IQueryHandler<in TQuery, TQueryResult> : IRequestHandler<TQuery, TQueryResult> 
    where TQuery : IQuery<TQueryResult> { }
