using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Exceptions;
using CarRentingSystem.Core.Models.Car;
using CarRentingSystem.Core.Services;

namespace CarRentingSystem.UnitTest.UnitTests
{
    public class CarServiceTests : UnitTestsBase
    {
        private ICarService carService;
        private IGuard guard;
        private IUserService userService;

        [OneTimeSetUp]
        public void SetUp() => this.carService = new CarService(this.repository, this.guard, this.userService);

        [Test]
        public async Task LastThreeCars_ShouldReturnCorrectData()
        {
            var result = await this.carService.LastThreeCars();
            var expectedFirstCar = result.FirstOrDefault();
            var expectedCarCount = this.context.Cars.Count();

            Assert.IsNotNull(result);
            Assert.IsNotNull(expectedFirstCar);
            Assert.That(result.Count(), Is.EqualTo(expectedCarCount));
            Assert.That(expectedFirstCar.Id, Is.EqualTo(this.NonRentedCar.Id));
            Assert.That(expectedFirstCar.Title, Is.EqualTo(this.NonRentedCar.Title));
            Assert.That(expectedFirstCar.ImageUrl, Is.EqualTo(this.NonRentedCar.ImageUrl));
            Assert.That(expectedFirstCar.Address, Is.EqualTo(this.NonRentedCar.Address));
        }

        [Test]
        public async Task AllCategories_ShouldReturnNotNullForResult_WithCorrectData()
        {
            var result = await this.carService.AllCategories();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AllCategories_ShouldReturnNotNullForFirstEC_WithCorrectData()
        {
            var result = await this.carService.AllCategories();
            Assert.IsTrue(result.Any());
        }

        [Test]
        public async Task AllCategories_ShouldReturnCount_WithCorrectData()
        {
            var result = await this.carService.AllCategories();
            Assert.That(result.Count(), Is.EqualTo(this.context.Cars.Count()));
        }

        [Test]
        public async Task AllCategories_ShouldReturnFirstECData_WithCorrectData()
        {
            var result = await this.carService.AllCategories();

            Assert.That(result.FirstOrDefault().Id, Is.EqualTo(this.NonRentedCar.EngineCategoryId));
            Assert.That(result.FirstOrDefault().Fuel, Is.EqualTo(this.NonRentedCar.EngineCategory.Fuel));
        }

        [Test]
        public async Task AllCategories_ShouldReturnORIGINAL_WithCorrectData()
        {
            var result = await this.carService.AllCategories();
            var expectedEngineCategory = result.FirstOrDefault();

            Assert.IsNotNull(result);
            Assert.IsNotNull(expectedEngineCategory);
            Assert.That(result.Count(), Is.EqualTo(this.context.Cars.Count()));
            Assert.That(expectedEngineCategory.Id, Is.EqualTo(this.NonRentedCar.EngineCategoryId));
            Assert.That(expectedEngineCategory.Fuel, Is.EqualTo(this.NonRentedCar.EngineCategory.Fuel));
        }

        [Test]
        public async Task CategoryExist_ShouldReturnTrue_WithCorrectEngineCategory()
        {
            var result = await this.carService.CategoryExist(this.NonRentedCar.EngineCategoryId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task CategoryExist_ShouldReturnFalse_WithWrongEngineCategory()
        {
            var inCorrectEngineCategoryId = 55;
            var result = await this.carService.CategoryExist(inCorrectEngineCategoryId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task Create_ShouldReturnCorrectObjectId()
        {
            var testCarModel = new CarModel()
            {
                Address = this.NonRentedCar.Address,
                EngineCategoryId = this.NonRentedCar.EngineCategoryId,
                Description = this.NonRentedCar.Description,
                ImageUrl = this.NonRentedCar.ImageUrl,
                PricePerMonth = this.NonRentedCar.PricePerMonth,
                Title = this.NonRentedCar.Title
            };
            var expectedCount = this.context.Cars.Count();

            var result = await this.carService.Create(testCarModel, this.Agent.Id);

            Assert.That(result, Is.EqualTo(expectedCount + 1));
        }

        [Test]
        public async Task All_ShouldReturnCorrectTotalCarsCount()
        {
            var expectedCount = this.context.Cars.Count();

            var result = await this.carService.All();

            Assert.IsNotNull(result);

            Assert.That(result.TotalCarsCount, Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task All_ShouldReturnCorrectCarsData()
        {
            var expectedCount = this.context.Cars.Count() - 1;

            var result = await this.carService.All();

            Assert.That(result.Cars.Count(), Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task All_ShouldReturnCorrectDataForFirstCar()
        {
            var result = await this.carService.All();
            var car = result.Cars.FirstOrDefault();

            Assert.IsNotNull(car);
            Assert.That(car.Id, Is.EqualTo(this.NonRentedCar.Id));
            Assert.That(car.Address, Is.EqualTo(this.NonRentedCar.Address));
            Assert.That(car.ImageUrl, Is.EqualTo(this.NonRentedCar.ImageUrl));
            Assert.That(car.IsRented, Is.EqualTo(this.NonRentedCar.Renter == null ? false : true));
            Assert.That(car.PricePerMonth, Is.EqualTo(this.NonRentedCar.PricePerMonth));
            Assert.That(car.Title, Is.EqualTo(this.NonRentedCar.Title));

        }

        [Test]
        public async Task AllCategoriesNames_ShouldReturnCorrectData()
        {
            var result = await this.carService.AllCategoriesNames();
            var firstEngineCategory = result.FirstOrDefault();
            var expectedEngineCategoryCount = 2;

            Assert.IsNotNull(result);
            Assert.IsNotNull(firstEngineCategory);

            Assert.That(result.Count(), Is.EqualTo(expectedEngineCategoryCount));

            Assert.That(firstEngineCategory, Is.EqualTo(this.RentedCar.EngineCategory.Fuel));

        }

        [Test]
        public async Task AllCarsByAgentId_ShouldReturnCorrectData()
        {
            var result = await this.carService.AllCarsByAgentId(this.Agent.Id);
            var firstCar = result.FirstOrDefault();
            var expectedCount = 2;

            Assert.IsNotNull(result);
            Assert.IsNotNull(firstCar);

            Assert.That(result.Count(), Is.EqualTo(expectedCount));

            Assert.That(firstCar.Id, Is.EqualTo(this.RentedCar.Id));
            Assert.That(firstCar.Title, Is.EqualTo(this.RentedCar.Title));
            Assert.That(firstCar.Address, Is.EqualTo(this.RentedCar.Address));
            Assert.That(firstCar.ImageUrl, Is.EqualTo(this.RentedCar.ImageUrl));
            Assert.That(firstCar.PricePerMonth, Is.EqualTo(this.RentedCar.PricePerMonth));

            Assert.IsTrue(firstCar.IsRented);

        }

        [Test]
        public async Task AllCarsByUserId_ShouldReturnCorrectData_WithWrongUserId()
        {
            var result = await this.carService.AllCarsByUserId(this.User.Id);
            var firstCar = result.FirstOrDefault();
            var expectedCount = 0;

            Assert.IsNotNull(result);
            Assert.IsNull(firstCar);

            Assert.That(result.Count(), Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task AllCarsByUserId_ShouldReturnCorrectData_WithCorrectUserId()
        {
            var result = await this.carService.AllCarsByUserId(this.Renter.Id);
            var firstCar = result.FirstOrDefault();
            var expectedCount = 1;

            Assert.IsNotNull(result);
            Assert.IsNotNull(firstCar);

            Assert.That(result.Count(), Is.EqualTo(expectedCount));

            Assert.That(firstCar.Id, Is.EqualTo(this.RentedCar.Id));
            Assert.That(firstCar.Address, Is.EqualTo(this.RentedCar.Address));
            Assert.That(firstCar.ImageUrl, Is.EqualTo(this.RentedCar.ImageUrl));
            Assert.That(firstCar.PricePerMonth, Is.EqualTo(this.RentedCar.PricePerMonth));
            Assert.That(firstCar.Title, Is.EqualTo(this.RentedCar.Title));

            Assert.IsTrue(firstCar.IsRented);
        }

        [Test]
        public async Task CarExist_ShouldReturnTrue_WithCorrectCarId()
        {
            var result = await this.carService.CarExist(this.RentedCar.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task CarExist_ShouldReturnFalse_WithWrongCarId()
        {
            var testCarId = 555;
            var result = await this.carService.CarExist(testCarId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task CarDetailsById_ShouldReturnCorrectData_WithCorrectCarId()
        {
            var result = await this.carService.CarDetailsById(this.RentedCar.Id);

            Assert.IsNotNull(result);

            Assert.That(result.Id, Is.EqualTo(this.RentedCar.Id));
            Assert.That(result.Address, Is.EqualTo(this.RentedCar.Address));
            Assert.That(result.Category, Is.EqualTo(this.RentedCar.EngineCategory.Fuel));
            Assert.That(result.Description, Is.EqualTo(this.RentedCar.Description));
            Assert.That(result.ImageUrl, Is.EqualTo(this.RentedCar.ImageUrl));
            Assert.That(result.Title, Is.EqualTo(this.RentedCar.Title));
            Assert.That(result.Title, Is.EqualTo(this.RentedCar.Title));
            Assert.That(result.PricePerMonth, Is.EqualTo(this.RentedCar.PricePerMonth));

            Assert.IsNotNull(result.Agent);
            Assert.That(result.Agent.Email, Is.EqualTo(this.Agent.User.Email));
            Assert.That(result.Agent.PhoneNumber, Is.EqualTo(this.Agent.PhoneNumber));

            Assert.IsTrue(result.IsRented);
        }

        [Test]
        public async Task CarEdit_ShouldReturnNewData_WithCorrectCarId()
        {
            await this.carService
                .CarEdit(this.RentedCar.Id,
                this.RentedCar.Title,
                this.RentedCar.Address,
                this.RentedCar.Description,
                this.RentedCar.ImageUrl,
                this.RentedCar.PricePerMonth,
                this.RentedCar.EngineCategoryId);

            var resultCar = this.context.Cars.FirstOrDefault(c => c.Id == this.RentedCar.Id);

            Assert.IsNotNull(resultCar);

            Assert.That(resultCar.Id, Is.EqualTo(this.RentedCar.Id));
            Assert.That(resultCar.Title, Is.EqualTo(this.RentedCar.Title));
            Assert.That(resultCar.Address, Is.EqualTo(this.RentedCar.Address));
            Assert.That(resultCar.Description, Is.EqualTo(this.RentedCar.Description));
            Assert.That(resultCar.ImageUrl, Is.EqualTo(this.RentedCar.ImageUrl));
            Assert.That(resultCar.PricePerMonth, Is.EqualTo(this.RentedCar.PricePerMonth));
            Assert.That(resultCar.EngineCategoryId, Is.EqualTo(this.RentedCar.EngineCategoryId));
        }

        [Test]
        public async Task CarEditSecondImplementation_ShouldReturnNewData_WithCorrectCarId()
        {
            await this.carService
                .CarEdit(this.RentedCar.Id,
                new CarModel()
                {
                    Id = this.RentedCar.Id,
                    Address = this.RentedCar.Address,
                    Description = this.RentedCar.Description,
                    EngineCategoryId = this.RentedCar.EngineCategoryId,
                    ImageUrl = this.RentedCar.ImageUrl,
                    PricePerMonth = this.RentedCar.PricePerMonth,
                    Title = this.RentedCar.Title
                });

            var resultCar = this.context.Cars.FirstOrDefault(c => c.Id == this.RentedCar.Id);

            Assert.IsNotNull(resultCar);

            Assert.That(resultCar.Id, Is.EqualTo(this.RentedCar.Id));
            Assert.That(resultCar.Title, Is.EqualTo(this.RentedCar.Title));
            Assert.That(resultCar.Address, Is.EqualTo(this.RentedCar.Address));
            Assert.That(resultCar.Description, Is.EqualTo(this.RentedCar.Description));
            Assert.That(resultCar.ImageUrl, Is.EqualTo(this.RentedCar.ImageUrl));
            Assert.That(resultCar.PricePerMonth, Is.EqualTo(this.RentedCar.PricePerMonth));
            Assert.That(resultCar.EngineCategoryId, Is.EqualTo(this.RentedCar.EngineCategoryId));
        }

        [Test]
        public async Task HasAgentWithId_ShouldReturnTrue_WithCorrectCarIdAndUserId()
        {
            var result = await this.carService.HasAgentWithId(this.RentedCar.Id, this.Agent.User.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task HasAgentWithId_ShouldReturnFalse_WithWrongUserId()
        {
            var result = await this.carService.HasAgentWithId(this.RentedCar.Id, this.Renter.Id);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task HasAgentWithId_ShouldReturnFalse_WithWrongCarId()
        {
            var result = await this.carService.HasAgentWithId(this.NonRentedCar.Id, this.Renter.Id);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetCarCategoryId_ShouldReturnCorrectId_WithCorrectCarId()
        {
            var result = await this.carService.GetCarCategoryId(this.NonRentedCar.Id);

            Assert.That(result, Is.EqualTo(this.NonRentedCar.EngineCategoryId));
        }

        [Test]
        public async Task DeleteCar_ShouldDeleteCorrectCar_WithCorrectCarId()
        {
            var carsCount = this.context.Cars.Count();
            await this.carService.DeleteCar(this.NonRentedCar.Id);
            var actualCarCount = this.context.Cars.Count();

            Assert.That(carsCount, Is.EqualTo(actualCarCount));
            Assert.IsFalse(this.NonRentedCar.IsActive);
        }

        [Test]
        public async Task IsRented_ShouldReturnFalse_WithCorrectCarId()
        {
            Assert.IsNull(this.NonRentedCar.RenterId);
            Assert.IsFalse(await this.carService.IsRented(this.NonRentedCar.Id));
        }

        [Test]
        public async Task IsRented_ShouldReturnTrue_WithCorrectCarId()
        {
            Assert.IsNotNull(this.RentedCar.RenterId);
            Assert.IsTrue(await this.carService.IsRented(this.RentedCar.Id));
        }

        [Test]
        public async Task IsRentedByUserWithId_ShouldReturnTrue_WithCorrectCarIdAndUserId()
        {
            Assert.IsTrue(await this.carService.IsRentedByUserWithId(this.RentedCar.Id, this.Renter.Id));
        }

        [Test]
        public async Task IsRentedByUserWithId_ShouldReturnFalse_WithCorrectCarIdAndUserId()
        {
            Assert.IsFalse(await this.carService.IsRentedByUserWithId(this.NonRentedCar.Id, this.Renter.Id));
        }

        [Test]
        public async Task IsRentedByUserWithId_ShouldReturnThrowException_WithCorrectCarIdAndUserId()
        {
            var result = await this.carService.IsRentedByUserWithId(555, this.Renter.Id);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task RentCar_ShouldReturnCurrentCarIsRentedTrue_WithCorrectCarIdAndUserId()
        {
            var isRentedBefore = this.NonRentedCar.RenterId == null ? true : false;
            await this.carService.RentCar(this.NonRentedCar.Id, this.Renter.Id);
            var isRentedAfter = this.NonRentedCar.RenterId == null ? false : true;
            Assert.That(isRentedBefore, Is.EqualTo(isRentedAfter));
        }

        [Test]
        public async Task LeaveCar_ShouldReturnCurrentCarIsNotRentedTrue_WithCorrectCarId()
        {
            var isRentedBefore = this.RentedCar.RenterId != null ? true : false;
            await this.carService.LeaveCar(this.NonRentedCar.Id);
            var isRentedAfter = this.NonRentedCar.RenterId != null ? false : true;
            Assert.That(isRentedBefore, Is.EqualTo(isRentedAfter));
        }

        [Test]
        public async Task GetAllExistedCar_ShouldReturnAllCarsCount()
        {
            var expectedCarsCount = this.context.Cars.Count();

            var cars = await this.carService.GetAllExistedCar();

            Assert.That(cars.Count(), Is.EqualTo(expectedCarsCount));
        }

        [Test]
        public async Task GetAllExistedCar_ShouldReturnCorrectData()
        {
            var cars = await this.carService.GetAllExistedCar();
            var firstCar = cars.FirstOrDefault();

            Assert.IsNotNull(cars);
            Assert.IsNotNull(firstCar);
            Assert.That(firstCar.Id, Is.EqualTo(this.RentedCar.Id));

            Assert.IsTrue(firstCar.IsRented);

            Assert.That(firstCar.Title, Is.EqualTo(this.RentedCar.Title));
            Assert.That(firstCar.ImageUrl, Is.EqualTo(this.RentedCar.ImageUrl));
            Assert.That(firstCar.Address, Is.EqualTo(this.RentedCar.Address));
            Assert.That(firstCar.PricePerMonth, Is.EqualTo(this.RentedCar.PricePerMonth));
        }








    }
}
