using Intervals.Model.Intervals.ICU;
using Intervals.Service.Interface;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Intervals.Service
{
    public class IntervalsJSAPICommunicator : IIntervalsAPICommunicator
    {
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
                _encodedAccessToken = value;
            }
        }

        IJSRuntime _jSRuntime;

        public IntervalsJSAPICommunicator(IJSRuntime jsRuntime)
        {
            _jSRuntime = jsRuntime;
        }

        public async Task<Wellness> GetWellnessForDate(string targetDate)
        {
            await _jSRuntime.InvokeAsync<string>("makeIntervalsGetRequest", $"https://intervals.icu/api/v1/athlete/{UserId}", EncodedAccessToken);

            int state = -1;

            do
            {
                state = await _jSRuntime.InvokeAsync<int>("getCurrentHTTPState");
            }
            while (state != 4);

            var response = await _jSRuntime.InvokeAsync<string>("getCurrentHTTPResponse");

            return JsonSerializer.Deserialize<Wellness>(response);
        }

        public Task<Wellness> PutWellnessForDate(string targetDate, Wellness newWellness)
        {
            throw new NotImplementedException();
        }
    }
}
