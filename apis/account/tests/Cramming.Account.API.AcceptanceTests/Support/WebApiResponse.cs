using System.Net;

namespace Cramming.Account.API.AcceptanceTests.Support
{
    public class WebApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? ResponseMessage { get; set; }
    }

    public class WebApiResponse<TData> : WebApiResponse
    {
        public TData? ResponseData { get; set; }
    }
}
