using Intervals.Model.EliteHRV;
using Intervals.Model.Intervals.ICU;
using Intervals.Service;
using Intervals.Service.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class HRVReadingProcessorTests
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))] //Assert
        public async Task NoReadingsOfExpectedType()
        {
            //Arrange
            var processor = new HRVReadingProcessor(null);

            //Act
            await processor.Process(new List<HRVReading>(), "", "");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))] //Assert
        public async Task NoReadingsWithMorningReadinessValue()
        {
            //Arrange
            var processor = new HRVReadingProcessor(null);

            var readings = new List<HRVReading>()
            {
                new HRVReading()
                {
                    Type = HRVReadingProcessor.EXPECTED_READING_TYPE
                }
            };

            //Act
            await processor.Process(readings, "", "");
        }

        [TestMethod]
        public void ReadingsPassedToCommunicationLayer_WellnessUpdatedWithCorrectValues()
        {
            //Arrange
            var mockedIntervalsAPICommunicator = new Mock<IIntervalsAPICommunicator>();
            mockedIntervalsAPICommunicator.Setup(api => api.GetWellnessForDate(It.IsAny<string>())).ReturnsAsync(GenerateWellnessEntry());
            mockedIntervalsAPICommunicator.Setup(api => api.PutWellnessForDate(It.IsAny<string>(), It.IsAny<Wellness>())).ReturnsAsync(GenerateWellnessEntry());

            var processor = new HRVReadingProcessor(mockedIntervalsAPICommunicator.Object);

            var readings = new List<HRVReading>()
            {
                GenerateHRVReading()
            };

            //Act
            var processorResult = processor.Process(readings, "", "");

            //Assert
            var targetDate = readings[0].DateTimeStart.ToString("yyyy-MM-dd");
            mockedIntervalsAPICommunicator.Verify(api => api.GetWellnessForDate(It.Is<string>(s => s == targetDate)), Times.Once);
            mockedIntervalsAPICommunicator.Verify(api => api.PutWellnessForDate(It.Is<string>(s => s == targetDate),
                                                                                It.Is<Wellness>(w => w.Readiness.ToString() == readings[0].MorningReadiness.ToString() &&
                                                                                                    w.Hrv.ToString() == readings[0].Rmssd.ToString() &&
                                                                                                    w.HrvSdnn.ToString() == readings[0].Sdnn.ToString())
                                                                                ), Times.Once);
        }

        private HRVReading GenerateHRVReading()
        {
            var r = new Random();

            return new HRVReading()
            {
                Type = HRVReadingProcessor.EXPECTED_READING_TYPE,
                DateTimeStart = DateTime.Now,
                MorningReadiness = r.Next(0, 10),
                Sdnn = r.Next(75, 125),
                Rmssd = r.Next(25, 75)
            };
        }

        private Wellness GenerateWellnessEntry()
        {
            return new Wellness();
        }
    }
}
