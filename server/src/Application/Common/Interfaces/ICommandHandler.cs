using MediatR;

namespace Application.Common.Interfaces;

public interface ICommandHandler<in TCommand, TCommandResult> : IRequestHandler<TCommand, TCommandResult>
    where TCommand : ICommand<TCommandResult> { }
