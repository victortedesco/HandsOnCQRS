using MediatR;

namespace HandsOnCQRS.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<TResponse>;