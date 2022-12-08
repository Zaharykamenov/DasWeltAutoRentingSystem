using CarRentingSystem.Core.Enums;
using CarRentingSystem.Core.Models.Car;
using CarRentingSystem.Core.Models.EngineCategory;

namespace CarRentingSystem.Core.Contracts
{
    public interface ICarService
    {
        Task<IEnumerable<CarHomeModel>> LastThreeCars();
        Task<IEnumerable<CarCategoryModel>> AllCategories();
        Task<bool> CategoryExist(int engineCategoryId);
        Task<int> Create(CarModel model, int agentId);
        Task<CarQueryModel> All(
            string? fuel = null,
            string? searchTerm = null,
            CarSorting CarSorting = CarSorting.Newest,
            int currentPage = 1,
            int CarsPerPage = 1);

        Task<IEnumerable<string>> AllCategoriesNames();

        Task<IEnumerable<CarServiceModel>> AllCarsByAgentId(int agentId);
        Task<IEnumerable<CarServiceModel>> AllCarsByUserId(string userId);
        Task<bool> CarExist(int id);
        Task<CarDetailsModel> CarDetailsById(int id);
        Task CarEdit(
            int carId,
            string title,
            string address,
            string description,
            string imageUrl,
            decimal pricePerMonth,
            int categoryId);

        Task CarEdit(int carId, CarModel carModel);

        Task<bool> HasAgentWithId(int carId, string currentUserId);

        Task<int> GetCarCategoryId(int carId);

        Task DeleteCar(int carId);

        Task<bool> IsRented(int carId);
        Task<bool> IsRentedByUserWithId(int carId, string userId);

        Task RentCar(int carId, string userId);

        Task LeaveCar(int carId);

        Task<IEnumerable<CarServiceModel>> GetAllExistedCar();

        


    }
}
