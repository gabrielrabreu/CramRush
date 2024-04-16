using MediatR;

namespace Cramming.SharedKernel
{
    public interface IQuery<out TResponse>
        : IRequest<TResponse>;
}
