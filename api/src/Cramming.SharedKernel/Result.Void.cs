using System.Net;

namespace Cramming.SharedKernel
{
    public class Result : Result<Result>
    {
        protected internal Result(HttpStatusCode status) : base(status) { }

        public static Result NotFound()
        {
            return new Result(HttpStatusCode.NotFound);
        }

        public static Result OK()
        {
            return new Result(HttpStatusCode.OK);
        }
    }
}
