using MediatR;

namespace Core.Cqs;

public interface IQuery<out TResponse> : IRequest<TResponse> {}
