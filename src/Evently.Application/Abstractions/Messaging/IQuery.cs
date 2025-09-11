using Evently.Domain.Abstractions;
using MediatR;

namespace Evently.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;