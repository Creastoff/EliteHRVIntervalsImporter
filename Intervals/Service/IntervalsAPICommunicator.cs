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
        public string UserId { get; set; }
        private string _encodedAccessToken;
        public string EncodedAccessToken
        {
            get
            {
                return _encodedAccessToken;
            }
            set
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", value);
                _encodedAccessToken = value;
            }
        }


        public IntervalsAPICommunicator(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient();
        }

        public async Task<Wellness> GetWellnessForDate(string date)
        {
            var response = await httpClient.GetAsync($"{UserId}/wellness/{date}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsObjectAsync<Wellness>();
        }

        public async Task<Wellness> PutWellnessForDate(string date, Wellness newWellness)
        {
            var response = await httpClient.PutAsync<Wellness>($"{UserId}/wellness/{date}", newWellness);

            var cont = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsObjectAsync<Wellness>();
        }
    }
}
