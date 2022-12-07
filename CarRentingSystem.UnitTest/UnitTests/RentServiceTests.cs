using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentingSystem.UnitTest.UnitTests
{
    public class RentServiceTests : UnitTestsBase
    {
        private IRentService rentService;

        [OneTimeSetUp]
        public void SetUp() => this.rentService = new RentService(this.repository);

        [Test]
        public async Task All_ShouldReturnCorrectListOfModel_WithCorrectData()
        {
            var result = await this.rentService.All();
            var resultCar = result.ToList().Find(c => c.CarTitle == this.RentedCar.Title);

            var expectedCount = this.context.Cars.Count(c => c.RenterId != null);

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(expectedCount));
            Assert.That(resultCar.RenterEmail, Is.EqualTo(this.RentedCar.Renter.Email));
            Assert.That(resultCar.AgentEmail, Is.EqualTo(this.Agent.User.Email));
            Assert.That(resultCar.CarImageUrl, Is.EqualTo(this.RentedCar.ImageUrl));
            Assert.That(resultCar.CarTitle, Is.EqualTo(this.RentedCar.Title));
            Assert.That(resultCar.AgentPhoneNumber, Is.EqualTo(this.Agent.PhoneNumber));
            Assert.That(resultCar.RenterFullName, Is.EqualTo($"{this.Renter.FirstName} {this.Renter.LastName}"));
            Assert.That(resultCar.AgentFullName, Is.EqualTo($"{this.Agent.User.FirstName} {this.Agent.User.LastName}"));
        }
    }
}
