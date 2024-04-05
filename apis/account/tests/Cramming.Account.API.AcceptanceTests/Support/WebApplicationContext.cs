using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Cramming.Account.API.AcceptanceTests.Support
{
    public class WebApplicationContext(CustomWebApplicationFactory factory, AuthContext authContext)
    {
        private readonly HttpClient _httpClient = factory.CreateClient();

        public WebApiResponse? WebApiResponse { get; private set; }

        public Task<WebApiResponse<TData>> ExecuteGetAsync<TData>(string endpoint)
        {
            return ExecuteSendAsync<TData>(HttpMethod.Get, endpoint);
        }

        public Task<WebApiResponse<TData>> ExecutePostAsync<TData>(string endpoint, object requestData)
        {
            return ExecuteSendAsync<TData>(HttpMethod.Post, endpoint, requestData);
        }

        public Task<WebApiResponse> ExecutePutAsync(string endpoint, object requestData)
        {
            return ExecuteSendAsync(HttpMethod.Put, endpoint, requestData);
        }

        public Task<WebApiResponse> ExecuteDeleteAsync(string endpoint)
        {
            return ExecuteSendAsync(HttpMethod.Delete, endpoint);
        }

        private async Task<WebApiResponse<TData>> ExecuteSendAsync<TData>(HttpMethod httpMethod, string endpoint, object? requestData = null)
        {
            var request = CreateRequest(httpMethod, endpoint, requestData);

            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            TData? responseData = default;

            try
            {
                responseData = JsonConvert.DeserializeObject<TData>(responseContent);
            }
            catch (Exception)
            {
            }

            var webApiResponse = new WebApiResponse<TData>
            {
                StatusCode = response.StatusCode,
                ResponseMessage = responseContent,
                ResponseData = responseData
            };

            WebApiResponse = webApiResponse;

            return webApiResponse;
        }


        private async Task<WebApiResponse> ExecuteSendAsync(HttpMethod httpMethod, string endpoint, object? requestData = null)
        {
            var request = CreateRequest(httpMethod, endpoint, requestData);

            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            var webApiResponse = new WebApiResponse
            {
                StatusCode = response.StatusCode,
                ResponseMessage = responseContent
            };

            WebApiResponse = webApiResponse;

            return webApiResponse;
        }

        private HttpRequestMessage CreateRequest(HttpMethod httpMethod, string endpoint, object? requestData = null)
        {
            var request = new HttpRequestMessage(httpMethod, endpoint);

            if (requestData != null)
            {
                var requestJsonContent = JsonConvert.SerializeObject(requestData);
                var requestHttpContent = new StringContent(requestJsonContent, Encoding.UTF8, "application/json");
                request.Content = requestHttpContent;
            }

            if (!string.IsNullOrEmpty(authContext.AccessToken))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authContext.AccessToken);

            return request;
        }
    }

}
