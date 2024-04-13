using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Cramming.SharedKernel
{
    public class LoggingBehaviour<TRequest, TResponse>(ILogger<Mediator> logger)
        : IPipelineBehavior<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling {RequestName}",
                typeof(TRequest).Name);

            var sw = Stopwatch.StartNew();

            var response = await next();

            logger.LogInformation("Handled {RequestName} with {Response} in {Ms} ms",
                typeof(TRequest).Name,
                response,
                sw.ElapsedMilliseconds);

            sw.Stop();

            return response;
        }
    }
}
