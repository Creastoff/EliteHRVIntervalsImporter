using Intervals.Model.EliteHRV;
using Intervals.Model.Intervals.ICU;
using Intervals.Service.Interface;

namespace Intervals.Service
{
    public class HRVReadingProcessor
    {
        public static string EXPECTED_READING_TYPE = "readiness";

        private IIntervalsAPICommunicator intervalsAPICommunicator;

        public HRVReadingProcessor(IIntervalsAPICommunicator _intervalsAPICommunicator)
        {
            intervalsAPICommunicator = _intervalsAPICommunicator;
        }

        public async Task<bool> Process(List<HRVReading> readings)
        {
            var processableHRVReadings = readings.Where(r => r.Type == EXPECTED_READING_TYPE).ToList();

            if (!processableHRVReadings.Any())
            {
                throw new Exception($"No readings found with type {EXPECTED_READING_TYPE}");
            }

            processableHRVReadings = processableHRVReadings.Where(r => r.MorningReadiness != null).ToList();

            if (!processableHRVReadings.Any())
            {
                throw new Exception($"No readings found with a readiness value. Note that readiness values are only available after the first few days of readings.");
            }

            foreach (var hrvReading in processableHRVReadings)
            {
                string targetDate = hrvReading.DateTimeStart.ToString("yyyy-MM-dd");

                var backupWellness = await intervalsAPICommunicator.GetWellnessForDate(targetDate);
                var newWellness = backupWellness.Clone();

                newWellness.RestingHr = float.Parse(hrvReading.HR.ToString());
                newWellness.Readiness = int.Parse(hrvReading.MorningReadiness.ToString());
                newWellness.HrvSdnn = float.Parse(hrvReading.Sdnn.ToString());
                newWellness.Hrv = float.Parse(hrvReading.Rmssd.ToString());

                var updatedWellness = await intervalsAPICommunicator.PutWellnessForDate(targetDate, newWellness);
            }

            return true;
        }
    }
}
