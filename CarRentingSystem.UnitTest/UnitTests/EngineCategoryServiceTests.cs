using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentingSystem.UnitTest.UnitTests
{
    public class EngineCategoryServiceTests : UnitTestsBase
    {
        private IEngineCategoryService engineCategoryService;

        [OneTimeSetUp]
        public void SetUp() => this.engineCategoryService = new EngineCategoryService(this.repository);

        [Test]
        public async Task CreateEngineCategory_ShouldReturnCorrectId()
        {
            var count = await this.context.EngineCategory
                .Where(e=>e.IsActive)
                .CountAsync();
            var resultId = await this.engineCategoryService
                .CreateEngineCategory(new Core.Models.EngineCategory.EngineCategoryCreateModel()
            {
                Fuel="TestFuel",
                Description = "This is a Test Description!"
            });

            var expectedId = count + 1;
            var expectedFuel = "TestFuel";
            var expectedDescription = "This is a Test Description!";

            var lastAddedEngineCategory = this.context.EngineCategory.LastOrDefault();

            Assert.That(resultId, Is.EqualTo(expectedId));
            Assert.IsNotNull(lastAddedEngineCategory);
            Assert.That(lastAddedEngineCategory.Fuel, Is.EqualTo(expectedFuel));
            Assert.That(lastAddedEngineCategory.Description, Is.EqualTo(expectedDescription));
            Assert.IsTrue(lastAddedEngineCategory.IsActive);
            Assert.That(lastAddedEngineCategory.Cars.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task DeleteEngineCategory_ShouldReturnCorrectCount()
        {
            var count = await this.context.EngineCategory
                .Where(e => e.IsActive)
                .CountAsync();

            await this.engineCategoryService.DeleteEngineCategory(count);

            Assert.That(count - 1, Is.EqualTo(this.context.EngineCategory.Where(e => e.IsActive).Count()));
        }

        [Test]
        public async Task GetAllEngineCategory_ShouldReturnCorrectData()
        {
            var count = this.context
                .EngineCategory
                .Where(e => e.IsActive)
                .Count();

            var engines = await this.engineCategoryService.GetAllEngineCategory();
            var firstEngine = engines.FirstOrDefault();

            Assert.IsNotNull(engines);
            Assert.IsNotNull(firstEngine);

            Assert.That(engines.Count(), Is.EqualTo(count));
            Assert.That(this.RentedCar.EngineCategory.Id, Is.EqualTo(firstEngine.Id));
            Assert.That(this.RentedCar.EngineCategory.Fuel, Is.EqualTo(firstEngine.Fuel));
            Assert.That(this.RentedCar.EngineCategory.Description, Is.EqualTo(firstEngine.Description));
            
            Assert.IsTrue(this.RentedCar.EngineCategory.IsActive);

        }

        [Test]
        public async Task GetEngineCategoryById_ShouldReturnCorrectData()
        {
            var engine = await this.engineCategoryService.GetEngineCategoryById(this.RentedCar.EngineCategoryId);
            
            Assert.IsNotNull(engine);
            Assert.That(engine.Id, Is.EqualTo(this.RentedCar.EngineCategoryId));
            Assert.That(engine.Fuel, Is.EqualTo(this.RentedCar.EngineCategory.Fuel));
            Assert.That(engine.Description, Is.EqualTo(this.RentedCar.EngineCategory.Description));

        }

        [Test]
        public async Task IsExistEngineCategoryById_ShouldReturnTrue()
        {
            var result = await this.engineCategoryService.IsExistEngineCategoryById(this.RentedCar.EngineCategoryId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsExistEngineCategoryById_ShouldReturnFalse()
        {
            var result = await this.engineCategoryService.IsExistEngineCategoryById(555);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task UpdateEngineCategory_ShouldReturnCorrectData()
        {
            var fuel = this.RentedCar.EngineCategory.Fuel;
            var description = this.RentedCar.EngineCategory.Description;
            var isActive = this.RentedCar.EngineCategory.IsActive;
            var id = this.RentedCar.EngineCategory.Id;

            await this.engineCategoryService.UpdateEngineCategory(new Core.Models.EngineCategory.EngineCategoryDetailsModel()
            {
                Id = id,
                Description = "TEST",
                Fuel = "TEST"
            });

            var engineAfter = this.RentedCar.EngineCategory;

            Assert.IsNotNull(engineAfter);

            Assert.That(engineAfter.Id, Is.EqualTo(id));
            Assert.That(engineAfter.Fuel, Is.Not.EqualTo(fuel));
            Assert.That(engineAfter.Description, Is.Not.EqualTo(description));
            Assert.That(engineAfter.IsActive, Is.EqualTo(isActive));
        }
    }
}
