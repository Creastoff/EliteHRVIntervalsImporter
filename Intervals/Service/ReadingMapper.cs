using CsvHelper;
using Intervals.Map;
using Intervals.Model.EliteHRV;
using Intervals.Service.Interface;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Intervals.Service
{
    [ExcludeFromCodeCoverage]
    public class ReadingMapper : IReadingMapper
    {
        public List<HRVReading> MapStreamContentsToList(StreamReader reader)
        {
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<ReadingMap>();
                var records = csv.GetRecords<HRVReading>();
                return records.ToList();
            }
        }
    }
}
