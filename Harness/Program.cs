using Intervals.Model.EliteHRV;
using Intervals.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace Harness
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var filePath = @"C:\full\path\to\csv.csv";
            var userId = config.GetValue<string>("UserId");
            var apiKey = config.GetValue<string>("ApiKey");

            if(string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("Insert UserId & ApiKey values into appsettings.json");
            }

            string accessToken = $"API_KEY:{apiKey}";
            var plainTextBytes = System.Text.Encoding.ASCII.GetBytes(accessToken);
            accessToken = Convert.ToBase64String(plainTextBytes);

            var readings = GetReadings(filePath);
            var processResult = Process(readings, userId, accessToken);
            processResult.Wait();
        }

        private async static Task<bool> Process(List<HRVReading> readings, string id, string accessToken)
        {
            var serviceProvider = new ServiceCollection();
            serviceProvider.AddHttpClient<IntervalsAPICommunicator>("", client =>
            {
                client.BaseAddress = new Uri($"https://intervals.icu/api/v1/athlete/{id}/wellness/");
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", accessToken);
                client.DefaultRequestHeaders.Add("Authorization", $"Basic {accessToken}");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            });
            serviceProvider.BuildServiceProvider();
            var service = serviceProvider.BuildServiceProvider();
            var httpClientFactory = service.GetRequiredService<IHttpClientFactory>();

            var apiCommunicator = new IntervalsAPICommunicator(httpClientFactory, id);
            var processor = new HRVReadingProcessor(apiCommunicator);
            return await processor.Process(readings);
        }

        private static List<HRVReading> GetReadings(string filePath)
        {
            var fileManager = new FileManager();
            var readingMapper = new ReadingMapper();

            var streamReader = fileManager.StreamReader(filePath);
            return readingMapper.MapStreamContentsToList(streamReader);
        }
    }
}
