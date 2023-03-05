using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Intervals.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class HttpContentExtension
    {
        public static async Task<T> ReadAsObjectAsync<T>(this HttpContent httpContent)
        {
            var content = await httpContent.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content);
        }
    }
}
