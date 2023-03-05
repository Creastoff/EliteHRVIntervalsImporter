using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;

namespace Intervals.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class HttpClientExtension
    {
        public static async Task<HttpResponseMessage> PutAsync<T>(this HttpClient httpClient, string requestUri, T dataToPost)
        {
            var json = JsonSerializer.Serialize(dataToPost);
            using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                return await httpClient.PutAsync(requestUri, stringContent);
            }
        }
    }
}
