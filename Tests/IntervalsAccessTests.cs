using Intervals.Model.Intervals.ICU;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class IntervalsAccessTests
    {
        [TestMethod]
        public async Task NoReadingsOfExpectedType()
        {
            //Arrange
            var model = new IntervalsAccess()
            {
                AccessToken = "helloworld"
            };

            //Act
            var result = model.GetEncodedAccessToken();

            //Assert
            var decodedString = Encoding.UTF8.GetString(Convert.FromBase64String(result));
            Assert.AreEqual(decodedString, "API_KEY:helloworld");
        }
    }
}
