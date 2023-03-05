using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Intervals.Model.Intervals.ICU
{
    [ExcludeFromCodeCoverage]
    internal static class WellnessHelpers
    {
        public static Wellness Clone(this Wellness original)
        {
            var json = JsonSerializer.Serialize(original);
            return JsonSerializer.Deserialize<Wellness>(json);
        }
    }
}