using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.Transport;
using CarRentingSystem.Core.Services;

namespace CarRentingSystem.UnitTest.UnitTests
{
    public class TransportServiceTests : UnitTestsBase
    {
        private ITransportService transportService;

        [OneTimeSetUp]
        public void SetUp() => this.transportService = new TransportService(this.repository);

        [Test]
        public async Task GetAllTransports_ShouldReturnCorrectData()
        {
            var count = this.context.Transports.Count();
            var transports = await this.transportService.GetAllTransports();

            Assert.IsNotNull(transports);
            Assert.That(count, Is.EqualTo(transports.Count()));
            Assert.IsNotNull(transports.FirstOrDefault());
            Assert.That(this.context.Transports.FirstOrDefault()?.PricePerKm, Is
                .EqualTo(transports.FirstOrDefault().PricePerKm));

            Assert.That(this.context.Transports.FirstOrDefault()?.CompanyName, Is
                .EqualTo(transports.FirstOrDefault().CompanyName));

            Assert.That(this.context.Transports.FirstOrDefault()?.DeliveryDays, Is
                .EqualTo(transports.FirstOrDefault().DeliveryDays));

            Assert.That(this.context.Transports.FirstOrDefault()?.Description, Is
                .EqualTo(transports.FirstOrDefault().Description));

            Assert.IsNotNull(transports.FirstOrDefault().Agent);

            Assert.That(this.context.Transports.FirstOrDefault().Agent.User.Email, Is
               .EqualTo(transports.FirstOrDefault().Agent.Email));

            Assert.That(this.context.Transports.FirstOrDefault().Agent.PhoneNumber, Is
               .EqualTo(transports.FirstOrDefault().Agent.PhoneNumber));

            Assert.That($"{this.context.Transports.FirstOrDefault().Agent.User.FirstName} {this.context.Transports.FirstOrDefault().Agent.User.LastName}", Is
               .EqualTo(transports.FirstOrDefault().Agent.FullName));
        }

        [Test]
        public async Task IsRentedByUserWithId_ShouldReturnTrue_WithCorrectUserAndId()
        {
            var result = await this
                .transportService
                .IsRentedByUserWithId(this.RentedTransport.Id, this.Renter.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsRentedByUserWithId_ShouldReturnFalse_WithInCorrectUserAndId()
        {
            var result = await this
                .transportService
                .IsRentedByUserWithId(this.NonRentedTransport.Id, this.Renter.Id);

            Assert.IsFalse(result);

            var result1 = await this
                .transportService
                .IsRentedByUserWithId(this.NonRentedTransport.Id, this.User.Id);

            Assert.IsFalse(result1);
        }

        [Test]
        public async Task RentTransport_ShouldReturnRentCurrentTransport()
        {
            var isRented = this.NonRentedTransport.RenterId;

            await this.transportService.RentTransport(this.NonRentedTransport.Id, this.User.Id);
            var result = this.NonRentedTransport.RenterId;

            Assert.That(isRented, Is.Not.EqualTo(result));
        }

        [Test]
        public void RentTransport_ShouldThrowInvalidOperationException_WithInCorrectTransportId()
        {
            var inCorrectTransportId = 555;
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await this.transportService.RentTransport(inCorrectTransportId, this.User.Id));

            Assert.That(ex.Message, Is.EqualTo("Transport was not found!"));
        }

        [Test]
        public async Task RentTransport_ShouldThrowArgumentException_WithAlreadyRentedTransport()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await this.transportService.RentTransport(this.RentedTransport.Id, this.User.Id));

            Assert.That(ex.Message, Is.EqualTo("Transport is already rented!"));
        }

        [Test]
        public async Task LeaveTransport_ShouldReturnNoRenterId_WithCorrectDataInput()
        {
            var renterId = this.RentedTransport.RenterId;
            await this.transportService.LeaveTransport(this.RentedTransport.Id);
            Assert.IsNull(this.RentedTransport.Renter);
            Assert.That(this.RentedTransport.RenterId, Is.Not.EqualTo(renterId));
        }

        [Test]
        public async Task TransportExist_ShouldReturnTrue_WithCorrectDataInputsAsync()
        {
            var result = await this.transportService.TransportExist(this.RentedTransport.Id);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task TransportExist_ShouldReturnFalse_WithInCorrectDataInputsAsync()
        {
            var result = await this.transportService.TransportExist(555);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task IsRented_ShouldReturnTrue_WithCorrectInputData()
        {
            Assert.IsTrue(await this.transportService.IsRented(this.RentedTransport.Id));
        }

        [Test]
        public async Task IsRented_ShouldReturnFalse_WithInCorrectInputData()
        {
            Assert.IsFalse(await this.transportService.IsRented(this.NonRentedTransport.Id));
        }

        [Test]
        public async Task AllTransportsByUserId_ShouldReturnOneRentedTransport_WithCorrectInput()
        {
            var transports = await this.transportService.AllTransportsByUserId(this.Renter.Id);
            Assert.That(transports.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task AllTransportsByUserId_ShouldReturnZeroRentedTransport_WithInCorrectInput()
        {
            var transports = await this.transportService.AllTransportsByUserId(this.Agent.User.Id);
            Assert.That(transports.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task TransportDetailsById_ShouldReturnCorrectExtraData_WithCorrectInputs()
        {
            var transport = await this.transportService.TransportDetailsById(this.RentedTransport.Id);

            Assert.That(transport.Id, Is.EqualTo(this.RentedTransport.Id));
            Assert.That(transport.DeliveryDays, Is.EqualTo(this.RentedTransport.DeliveryDays));
            Assert.That(transport.Description, Is.EqualTo(this.RentedTransport.Description));
            Assert.That(transport.PricePerKm, Is.EqualTo(this.RentedTransport.PricePerKm));
            Assert.That(transport.CompanyName, Is.EqualTo(this.RentedTransport.CompanyName));
            Assert.That(transport.ImageUrl, Is.EqualTo(this.RentedTransport.ImageUrl));
            Assert.IsTrue(transport.IsRented);

        }

        [Test]
        public async Task HasAgentWithId_ShouldReturnTrue_WithCorrectData()
        {
            Assert.IsTrue(await this.transportService.HasAgentWithId(this.RentedTransport.Id, this.Agent.User.Id));
        }

        [Test]
        public async Task HasAgentWithId_ShouldReturnFalse_WithInCorrectData()
        {
            Assert.IsFalse(await this.transportService.HasAgentWithId(this.RentedTransport.Id, this.Renter.Id));
        }

        [Test]
        public async Task HasRentedCarByUserId_ShouldReturnTrue_WithCorrectData()
        {
            Assert.IsTrue(await this.transportService.HasRentedCarByUserId(this.Renter.Id));
        }

        [Test]
        public async Task HasRentedCarByUserId_ShouldReturnFalse_WithInCorrectData()
        {
            Assert.IsFalse(await this.transportService.HasRentedCarByUserId(this.Agent.User.Id));
        }

        [Test]
        public async Task TransportEdit_ShouldReturnCorrectData_WithCorrectInputData()
        {
            var transport = this.context.Transports.FirstOrDefault(t => t.Id == this.RentedTransport.Id);
            Assert.IsNotNull(transport);
            await this.transportService.TransportEdit(this.RentedTransport.Id,
                new AddTransportModel()
                {
                    Id = this.RentedTransport.Id,
                    CompanyName = "Zahary",
                    DeliveryDays = 1,
                    Description = "Test",
                    ImageUrl = "xaxaxa",
                    PricePerKm = 100.0m
                });

            Assert.That(transport.DeliveryDays, Is.EqualTo(this.RentedTransport.DeliveryDays));
            Assert.That(transport.CompanyName, Is.EqualTo(this.RentedTransport.CompanyName));
            Assert.That(transport.Id, Is.EqualTo(this.RentedTransport.Id));
            Assert.That(transport.Description, Is.EqualTo(this.RentedTransport.Description));
            Assert.That(transport.ImageUrl, Is.EqualTo(this.RentedTransport.ImageUrl));
            Assert.That(transport.PricePerKm, Is.EqualTo(this.RentedTransport.PricePerKm));
        }

        [Test]
        public async Task DeleteTransport_ShouldReturnCountSmallerThanBefore_WithCorrectInputData()
        {
            var count = this.context.Transports.Count();
            await this.transportService.DeleteTransport(this.RentedTransport.Id);
            var newCount = this.context.Transports.Count();

            Assert.That(count, Is.EqualTo(newCount));
        }

        [Test]
        public async Task Create_ShouldReturnCorrectData_WithCorrectInputData()
        {
            var count = this.context.Transports.Count();

            var id = await this.transportService.Create(new AddTransportModel()
            {
                CompanyName = "Zahary",
                DeliveryDays = 5,
                Description = "Test",
                ImageUrl = "xaxax",
                PricePerKm = 10.0m
            }, this.Agent.Id);

            var newCount = this.context.Transports.Count();

            Assert.That(count, Is.EqualTo(newCount - 1));
            Assert.That(id, Is.EqualTo(newCount));
        }

        [Test]
        public async Task AllTransportsByAgentId_ShouldReturnCorrectData_WithCorrectInputData()
        {
            var count = this.context.Transports.Count(t => t.AgentId == this.Agent.Id);
            var allTransports = await this.transportService.AllTransportsByAgentId(this.Agent.Id);

            Assert.That(allTransports.Count(), Is.EqualTo(count));
        }














    }
}
