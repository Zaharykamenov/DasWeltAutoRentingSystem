using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentingSystem.UnitTest.UnitTests
{
    public class StatisticsServiceTests : UnitTestsBase
    {
        private IStatisticsService statisticsService;

        [OneTimeSetUp]
        public void SetUp() => this.statisticsService = new StatisticsService(this.repository);

        [Test]
        public async Task Total_ShouldReturnCorrect()
        {
            var result = await this.statisticsService.Total();

            var totalCars = this.context.Cars.Count();
            var totalRentedCars = this.context.Cars.Count(c => c.RenterId != null);

            Assert.IsNotNull(result);
            Assert.That(result.TotalCars, Is.EqualTo(totalCars));
            Assert.That(result.TotalRents, Is.EqualTo(totalRentedCars));
        }












    }
}
