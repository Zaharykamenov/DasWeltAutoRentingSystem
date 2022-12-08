using CarRentingSystem.Common;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.EngineCategory;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Core.Services
{
    public class EngineCategoryService : IEngineCategoryService
    {
        private readonly IRepository repository;

        public EngineCategoryService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<int> CreateEngineCategory(EngineCategoryCreateModel model)
        {
            var engine = new EngineCategory()
            {
                Fuel = model.Fuel,
                Description = model.Description,
                IsActive = true
            };

            await this.repository.AddAsync(engine);
            await this.repository.SaveChangesAsync();

            return engine.Id;
        }

        public async Task DeleteEngineCategory(int engineCategoryId)
        {
            var engine = await this.repository.GetByIdAsync<EngineCategory>(engineCategoryId);
            engine.IsActive = false;
            await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<EngineCategoryViewModel>> GetAllEngineCategory()
        {
            return await this.repository.AllReadonly<EngineCategory>()
                .Where(e => e.IsActive == true)
                .Select(e => new EngineCategoryViewModel()
                {
                    Id = e.Id,
                    Fuel = e.Fuel,
                    Description = e.Description,
                    Cars = this.repository
                        .AllReadonly<Car>()
                        .Count(c => c.EngineCategoryId == e.Id)

                })
                .ToListAsync();
        }

        public async Task<EngineCategoryDetailsModel> GetEngineCategoryById(int engineCategoryId)
        {

            var engineCategory = await this.repository.GetByIdAsync<EngineCategory>(engineCategoryId);

            return new EngineCategoryDetailsModel()
            {
                Id = engineCategory.Id,
                Fuel = engineCategory.Fuel,
                Description = engineCategory.Description
            };
        }

        public async Task<bool> IsExistEngineCategoryById(int engineCategoryId)
        {
            return await this.repository.AllReadonly<EngineCategory>()
                .Where(e => e.IsActive == true)
                .AnyAsync(e => e.Id == engineCategoryId);
        }

        public async Task UpdateEngineCategory(EngineCategoryDetailsModel model)
        {
            var engine = await this.repository.GetByIdAsync<EngineCategory>(model.Id);
            engine.Fuel = model.Fuel;
            engine.Description = model.Description;
            await this.repository.SaveChangesAsync();
        }


    }
}
