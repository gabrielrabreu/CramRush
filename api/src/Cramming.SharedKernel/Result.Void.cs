using System.Net;

namespace Cramming.SharedKernel
{
    public class Result : Result<Result>
    {
        protected internal Result(HttpStatusCode status) : base(status) { }

        public static Result OK()
        {
            return new Result(HttpStatusCode.OK);
        }

        public static Result BadRequest()
        {
            return new Result(HttpStatusCode.BadRequest);
        }

        public static Result NotFound()
        {
            return new Result(HttpStatusCode.NotFound);
        }
    }
}
