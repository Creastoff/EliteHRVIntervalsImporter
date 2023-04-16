using Intervals.Model.Intervals.ICU;

namespace Intervals.Service.Interface
{
    public interface IIntervalsAPICommunicator
    {
        public Task<Wellness> GetWellnessForDate(string targetDate);
        public Task<Wellness> PutWellnessForDate(string targetDate, Wellness newWellness);
        string UserId { get; set; }
        string EncodedAccessToken { get; set; }
    }
}
