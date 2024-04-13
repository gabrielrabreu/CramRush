using Xunit.Abstractions;

namespace Cramming.FunctionalTests.Support
{
    public static class HttpClientExtensionMethods
    {
        public static async Task<HttpResponseMessage> ExecuteGetAsync(
            this HttpClient client, 
            string route,
            ITestOutputHelper? output = null)
        {
            var response = await client.GetAsync(route);
            output?.LogHttpRequest("GET", route, response.StatusCode);
            return response;
        }

        public static async Task<HttpResponseMessage> ExecutePostAsync(
            this HttpClient client, 
            string route, 
            HttpContent content,
            ITestOutputHelper? output = null)
        {
            var response = await client.PostAsync(route, content);
            output?.LogHttpRequest("POST", route, response.StatusCode);
            return response;
        }

        public static async Task<HttpResponseMessage> ExecutePutAsync(
            this HttpClient client,
            string route,
            HttpContent content,
            ITestOutputHelper? output = null)
        {
            var response = await client.PutAsync(route, content);
            output?.LogHttpRequest("PUT", route, response.StatusCode);
            return response;
        }

        public static async Task<HttpResponseMessage> ExecuteDeleteAsync(
            this HttpClient client,
            string route,
            ITestOutputHelper? output = null)
        {
            var response = await client.DeleteAsync(route);
            output?.LogHttpRequest("DELETE", route, response.StatusCode);
            return response;
        }
    }
}
