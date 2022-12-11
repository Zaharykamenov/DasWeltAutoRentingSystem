using CarRentingSystem.Common;
using CarRentingSystem.Infrastructure.Data;
using CarRentingSystem.Infrastructure.Data.Models;
using CarRentingSystem.UnitTest.Mocks;

namespace CarRentingSystem.UnitTest.UnitTests
{
    public class UnitTestsBase
    {
        protected ApplicationDbContext context;
        protected IRepository repository;

        [OneTimeSetUp]
        public void SetUpBase()
        {
            this.context = DatabaseMock.Instance;
            this.repository = new Repository(this.context);
            this.SeedDatabase();
        }

        public User Renter { get; private set; }
        public User User { get; private set; }
        public Agent Agent { get; private set; }
        public Car RentedCar { get; private set; }
        public Car NonRentedCar { get; private set; }
        public Transport NonRentedTransport { get; private set; }
        public Transport RentedTransport { get; private set; }

        private void SeedDatabase()
        {
            this.Renter = new User()
            {
                Id = "RenterUserId",
                Email = "renter@mail.com",
                FirstName = "Renter",
                LastName = "Rentov"
            };

            this.context.Users.Add(this.Renter);

            this.User = new User()
            {
                Id = "AgentUserId",
                Email = "agenter@mail.com",
                FirstName = "Agenter",
                LastName = "Agentov"
            };

            this.context.Users.Add(this.User);

            this.Agent = new Agent()
            {
                PhoneNumber = "+359999999999",
                UserId = "AgentUserId",
                User = this.User
            };

            this.context.Agents.Add(this.Agent);

            this.RentedCar = new Car()
            {
                Title = "Test VW Amarok DC Aventura V6 TDI BMT 4MOTION",
                Address = "Test, 1000, test 3 st.",
                Description = "This is a test description. This is a test description. This is a test description.",
                ImageUrl = "https://d3t2kd17lko26w.cloudfront.net/7B4DFDEE62BA9B60F9EF19F3A831E308/images/cda9f12c-4871-4b86-a80a-ba51f32fcd21/webp/768",
                Renter = this.Renter,
                Agent = this.Agent,
                EngineCategory = new EngineCategory()
                {
                    Id = 1,
                    Fuel = "Diesel",
                    Description = "This is a test description.",
                    IsActive = true
                }
            };
            this.context.Cars.Add(this.RentedCar);

            this.NonRentedCar = new Car()
            {
                Title = "Test VW Caddy Maxi Trendline TSI 7 места",
                Address = "Test 2, 2000, test 2 st.",
                Description = "This is a test description. This is a test description. This is a test description.",
                ImageUrl = "https://d3t2kd17lko26w.cloudfront.net/B9FB37145759BF73D612D85021A9D19F/images/74bed130-890f-46e6-b878-dd5a57873d25/webp/768",
                Renter = null,
                Agent = this.Agent,
                EngineCategory = new EngineCategory()
                {
                    Id = 2,
                    Fuel = "Benzin",
                    Description = "This is a test description.",
                    IsActive = true
                }
            };
            this.context.Cars.Add(this.NonRentedCar);

            this.NonRentedTransport = new Transport()
            {
                Id = 1,
                CompanyName = "VidaTrans",
                DeliveryDays = 5,
                Description = "This is VidaTrans description!",
                ImageUrl = "https://www.vida.se/wp-content/uploads/2018/12/vida_rgb.jpg",
                IsActive = true,
                PricePerKm = 10.0m,
                AgentId = this.Agent.Id,
                Renter = null
            };
            this.context.Transports.Add(this.NonRentedTransport);

            this.RentedTransport = new Transport()
            {
                Id = 2,
                CompanyName = "SofiaTrans",
                DeliveryDays = 3,
                Description = "This is SofiaTrans description!",
                ImageUrl = "https://static.vecteezy.com/system/resources/thumbnails/006/604/979/small/transport-logo-with-modern-and-professional-concept-premium-vector.jpg",
                IsActive = true,
                PricePerKm = 20.0m,
                AgentId = this.Agent.Id,
                RenterId = this.Renter.Id
            };

            this.context.Transports.Add(this.RentedTransport);

            this.context.SaveChanges();

        }

        [OneTimeTearDown]
        public void TearDownBase() => this.context.Dispose();
    }
}
