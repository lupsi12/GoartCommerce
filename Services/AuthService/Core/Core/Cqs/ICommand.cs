using MediatR;

namespace Core.Cqs;

public interface ICommand<out TResult> : IRequest<TResult>
{
    
}