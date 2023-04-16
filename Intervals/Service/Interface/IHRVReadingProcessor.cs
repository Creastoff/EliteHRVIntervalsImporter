using Intervals.Model.EliteHRV;

namespace Intervals.Service.Interface
{
    public interface IHRVReadingProcessor
    {
        Task<bool> Process(List<HRVReading> readings, string id, string encodedAccessToken);
    }
}