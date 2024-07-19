using MediatR;

namespace HandsOnCQRS.Abstractions.Messaging;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, string>
    where TCommand : ICommand;

public interface ICommandHandler<TCommand, TResponse>
: IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
;
