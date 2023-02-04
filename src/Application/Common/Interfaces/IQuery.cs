using MediatR;

namespace Application.Common.Interfaces;

public interface IQuery<out TQueryResult> : IRequest<TQueryResult> { }
