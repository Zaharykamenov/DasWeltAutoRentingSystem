using CarRentingSystem.Core.Models.Car;
using CarRentingSystem.Core.Models.EngineCategory;
using CarRentingSystem.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentingSystem.Core.Contracts
{
    public interface IEngineCategoryService
    {
        Task<IEnumerable<EngineCategoryViewModel>> GetAllEngineCategory();
        Task<EngineCategoryDetailsModel> GetEngineCategoryById(int engineCategoryId);
        Task<bool> IsExistEngineCategoryById(int engineCategoryId);
        Task UpdateEngineCategory(EngineCategoryDetailsModel model);
        Task DeleteEngineCategory(int engineCategoryId);
        Task<int> CreateEngineCategory(EngineCategoryCreateModel model);
    }
}
