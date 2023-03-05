using System.Diagnostics.CodeAnalysis;

namespace Intervals.Model.EliteHRV
{
    [ExcludeFromCodeCoverage]
    public class HRVReading
    {
        public string Member { get; set; }
        public string Type { get; set; }
        public string Position { get; set; }
        public string BreathingPattern { get; set; }
        public DateTime DateTimeStart { get; set; }
        public string DateTimeEnd { get; set; }
        public double Duration { get; set; }
        public int HRV { get; set; }
        public int? MorningReadiness { get; set; }
        public string Balance { get; set; }
        public double HRVCV { get; set; }
        public double HR { get; set; }
        public double lnRmssd { get; set; }
        public double Rmssd { get; set; }
        public int Nn50 { get; set; }
        public int Pnn50 { get; set; }
        public double Sdnn { get; set; }
        public double LowFrequencyPower { get; set; }
        public double HighFrequencyPower { get; set; }
        public double LFHFRatio { get; set; }
        public double TotalPower { get; set; }
    }

}
