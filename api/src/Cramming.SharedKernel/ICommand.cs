using MediatR;

namespace Cramming.SharedKernel
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
