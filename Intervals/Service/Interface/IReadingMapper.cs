using Intervals.Model.EliteHRV;

namespace Intervals.Service.Interface
{
    public interface IReadingMapper
    {
        List<HRVReading> MapStreamContentsToList(StreamReader reader);
    }
}