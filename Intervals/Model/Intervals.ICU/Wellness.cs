using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Intervals.Model.Intervals.ICU
{
    [ExcludeFromCodeCoverage]
    public class Wellness
    {
        [JsonPropertyName("restingHR")]
        public object RestingHr { get; set; }

        [JsonPropertyName("hrv")]
        public object Hrv { get; set; }

        [JsonPropertyName("hrvSDNN")]
        public object HrvSdnn { get; set; }

        [JsonPropertyName("readiness")]
        public object Readiness { get; set; }
    }
}
