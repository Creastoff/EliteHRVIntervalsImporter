using Intervals.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;

namespace Intervals.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static void AddIntervalsHttpClient(this IServiceCollection serviceCollection, string baseAddress, string accessToken)
        {
            serviceCollection.AddHttpClient<IntervalsAPICommunicator>("", client =>
            {
                client.BaseAddress = new Uri(baseAddress);
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", accessToken);
                client.DefaultRequestHeaders.Add("Authorization", $"Basic {accessToken}");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            });
        }
    }
}
