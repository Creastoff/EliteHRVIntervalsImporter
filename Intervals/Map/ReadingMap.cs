using CsvHelper.Configuration;
using Intervals.Model.EliteHRV;
using System.Diagnostics.CodeAnalysis;

namespace Intervals.Map
{
    [ExcludeFromCodeCoverage]
    public class ReadingMap : ClassMap<HRVReading>
    {
        public ReadingMap()
        {
            Map(m => m.Member).Name("Member");
            Map(m => m.Type).Name("Type");
            Map(m => m.Position).Name("Position");
            Map(m => m.BreathingPattern).Name("Breathing Pattern");
            Map(m => m.DateTimeStart).Name("Date Time Start");
            Map(m => m.DateTimeEnd).Name("Date Time End");
            Map(m => m.Duration).Name("Duration");
            Map(m => m.HRV).Name("HRV");
            Map(m => m.MorningReadiness).Name("Morning Readiness");
            Map(m => m.Balance).Name("Balance");
            Map(m => m.HRVCV).Name("HRV CV");
            Map(m => m.HR).Name("HR");
            Map(m => m.lnRmssd).Name("lnRmssd");
            Map(m => m.Rmssd).Name("Rmssd");
            Map(m => m.Nn50).Name("Nn50");
            Map(m => m.Pnn50).Name("Pnn50");
            Map(m => m.Sdnn).Name("Sdnn");
            Map(m => m.LowFrequencyPower).Name("Low Frequency Power");
            Map(m => m.HighFrequencyPower).Name("High Frequency Power");
            Map(m => m.LFHFRatio).Name("LF/HF Ratio");
            Map(m => m.TotalPower).Name("Total Power");
        }
    }
}
