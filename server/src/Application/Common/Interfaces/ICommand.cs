using MediatR;

namespace Application.Common.Interfaces;

public interface ICommand : IRequest { } // todo convert to a Result

public interface ICommand<out TCommandResult> : IRequest<TCommandResult> { }
