using MediatR;

namespace HandsOnCQRS.Abstractions.Messaging;

public interface ICommand : IRequest<string>;

public interface ICommand<TResponse> : IRequest<TResponse>;