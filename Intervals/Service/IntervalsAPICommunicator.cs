using Intervals.Extensions;
using Intervals.Model.Intervals.ICU;
using Intervals.Service.Interface;
using System.Diagnostics.CodeAnalysis;

namespace Intervals.Service
{
    [ExcludeFromCodeCoverage]
    public class IntervalsAPICommunicator : IIntervalsAPICommunicator
    {
        HttpClient httpClient;

        public IntervalsAPICommunicator(IHttpClientFactory httpClientFactory, string id)
        {
            httpClient = httpClientFactory.CreateClient();
        }

        public async Task<Wellness> GetWellnessForDate(string date)
        {
            var response = await httpClient.GetAsync(date);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsObjectAsync<Wellness>();
        }

        public async Task<Wellness> PutWellnessForDate(string date, Wellness newWellness)
        {
            var response = await httpClient.PutAsync<Wellness>(date, newWellness);

            var cont = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsObjectAsync<Wellness>();
        }
    }
}
