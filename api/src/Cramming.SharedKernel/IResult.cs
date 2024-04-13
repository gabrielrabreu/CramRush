using System.Net;

namespace Cramming.SharedKernel
{
    public interface IResult
    {
        HttpStatusCode Status { get; }
    }
}
