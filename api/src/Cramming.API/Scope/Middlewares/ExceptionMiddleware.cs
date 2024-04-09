using Microsoft.AspNetCore.Mvc;

namespace Cramming.API.Scope.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;

        public ExceptionMiddleware()
        {
            _exceptionHandlers = new()
            {
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException }
            };
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var exceptionType = exception.GetType();
                if (_exceptionHandlers.TryGetValue(exceptionType, out Func<HttpContext, Exception, Task>? value))
                    await value.Invoke(context, exception);
                else
                    throw;
            }
        }

        private async Task HandleUnauthorizedAccessException(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
            });
        }
    }
}
