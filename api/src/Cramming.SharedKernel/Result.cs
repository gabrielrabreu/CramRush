using System.Net;

namespace Cramming.SharedKernel
{
    public class Result<T> : IResult
    {
        public T Value { get; init; }

        public HttpStatusCode Status { get; protected set; } = HttpStatusCode.OK;

        public bool IsSuccess => Status == HttpStatusCode.OK;

        protected Result()
        {
        }

        protected Result(HttpStatusCode status)
        {
            Status = status;
        }

        public Result(T value)
        {
            Value = value;
        }

        public static implicit operator Result<T>(T value) => new(value);

        public static implicit operator Result<T>(Result result) => new()
        {
            Status = result.Status,
        };
    }
}
