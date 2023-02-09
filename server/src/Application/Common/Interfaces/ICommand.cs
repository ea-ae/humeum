using MediatR;

namespace Application.Common.Interfaces;

public interface ICommand<out TCommandResult> : IRequest<TCommandResult> { }
