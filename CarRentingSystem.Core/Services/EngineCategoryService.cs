using CarRentingSystem.Common;
using CarRentingSystem.Core.Constants;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.EngineCategory;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Core.Services
{
    /// <summary>
    /// That class represent all method related to car engine category.
    /// </summary>
    public class EngineCategoryService : IEngineCategoryService
    {
        /// <summary>
        /// Private property repository
        /// </summary>
        private readonly IRepository repository;

        /// <summary>
        /// Constructor of the class EngineCategoryService
        /// </summary>
        /// <param name="repository"></param>
        public EngineCategoryService(IRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Return integer that represent ID for created car engine category in database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<int> CreateEngineCategory(EngineCategoryCreateModel model)
        {
            if (model==null)
            {
                throw new ArgumentNullException(EngineCategoryConstants.ParametersAreNullOrEmptyError);
            }
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

        /// <summary>
        /// Method make isActive property to false of current engine category ID.
        /// </summary>
        /// <param name="engineCategoryId"></param>
        /// <returns></returns>
        public async Task DeleteEngineCategory(int engineCategoryId)
        {
            var engine = await this.repository.GetByIdAsync<EngineCategory>(engineCategoryId);
            engine.IsActive = false;
            await this.repository.SaveChangesAsync();
        }

        /// <summary>
        /// Return IEnumerable of EngineCategoryViewModel represent all engine category.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Return EngineCategoryDetailsModel represent engine category by ID from database.
        /// </summary>
        /// <param name="engineCategoryId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return true or false if engine category ID exist in database.
        /// </summary>
        /// <param name="engineCategoryId"></param>
        /// <returns></returns>
        public async Task<bool> IsExistEngineCategoryById(int engineCategoryId)
        {
            return await this.repository.AllReadonly<EngineCategory>()
                .Where(e => e.IsActive == true)
                .AnyAsync(e => e.Id == engineCategoryId);
        }

        /// <summary>
        /// Method modify engine category in database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>No Returns</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateEngineCategory(EngineCategoryDetailsModel model)
        {
            if (model==null)
            {
                throw new ArgumentNullException(EngineCategoryConstants.ParametersAreNullOrEmptyError);
            }

            var engine = await this.repository.GetByIdAsync<EngineCategory>(model.Id);
            engine.Fuel = model.Fuel;
            engine.Description = model.Description;
            await this.repository.SaveChangesAsync();
        }


    }
}
