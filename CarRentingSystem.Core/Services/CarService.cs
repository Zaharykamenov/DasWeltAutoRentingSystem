using CarRentingSystem.Common;
using CarRentingSystem.Core.Constants;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Enums;
using CarRentingSystem.Core.Exceptions;
using CarRentingSystem.Core.Models.Agent;
using CarRentingSystem.Core.Models.Car;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Core.Services
{
    public class CarService : ICarService
    {
        private readonly IRepository repository;
        private readonly IGuard guard;
        private readonly IUserService userService;

        public CarService(IRepository repository, IGuard guard, IUserService userService)
        {
            this.repository = repository;
            this.guard = guard;
            this.userService = userService;
        }

        public async Task<CarQueryModel> All(string? fuel = null, string? searchTerm = null, CarSorting carSorting = CarSorting.Newest, int currentPage = 1, int carsPerPage = 1)
        {
            var result = new CarQueryModel();
            var Cars = this.repository
                .AllReadonly<Car>()
                .Where(h => h.IsActive);

            if (String.IsNullOrEmpty(fuel) == false)
            {
                Cars = Cars
                    .Where(h => h.EngineCategory.Fuel == fuel);
            }

            if (String.IsNullOrEmpty(searchTerm) == false)
            {
                searchTerm = $"%{searchTerm.ToLower()}%";

                Cars = Cars
                    .Where(h => EF.Functions.Like(h.Title.ToLower(), searchTerm)
                    || EF.Functions.Like(h.Address.ToLower(), searchTerm)
                    || EF.Functions.Like(h.Description.ToLower(), searchTerm));
            }

            switch (carSorting)
            {
                case CarSorting.Price:
                    Cars = Cars
                        .OrderBy(h => h.PricePerMonth);
                    break;
                case CarSorting.NotRentedFirst:
                    Cars = Cars
                        .OrderBy(h => h.RenterId);
                    break;
                default:
                    Cars = Cars.OrderByDescending(h => h.Id);
                    break;
            }

            //pagination
            result.Cars = await Cars
                .Skip((currentPage - 1) * carsPerPage)
                .Take(carsPerPage)
                .Select(h => new CarServiceModel()
                {
                    Address = h.Address,
                    Id = h.Id,
                    ImageUrl = h.ImageUrl,
                    IsRented = h.RenterId != null,
                    PricePerMonth = h.PricePerMonth,
                    Title = h.Title
                })
                .ToListAsync();

            result.TotalCarsCount = await Cars.CountAsync();

            return result;
        }

        public async Task<IEnumerable<CarCategoryModel>> AllCategories()
        {
            return await this.repository.AllReadonly<EngineCategory>()
                 .Where(c => c.IsActive == true)
                 .OrderBy(c => c.Fuel)
                 .Select(c => new CarCategoryModel()
                 {
                     Id = c.Id,
                     Fuel = c.Fuel
                 })
                 .ToListAsync();
        }

        public async Task<IEnumerable<string>> AllCategoriesNames()
        {
            return await this.repository.AllReadonly<EngineCategory>()
                .Where(e => e.IsActive)
                .Select(c => c.Fuel)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<CarServiceModel>> AllCarsByAgentId(int agentId)
        {
            return await this.repository.AllReadonly<Car>()
                 .Where(h => h.IsActive)
                 .Where(h => h.AgentId == agentId)
                 .Select(h => new CarServiceModel()
                 {
                     Address = h.Address,
                     Id = h.Id,
                     ImageUrl = h.ImageUrl,
                     PricePerMonth = h.PricePerMonth,
                     Title = h.Title,
                     IsRented = h.RenterId != null

                 })
                 .ToListAsync();
        }

        public async Task<IEnumerable<CarServiceModel>> AllCarsByUserId(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(CarConstants.ParametersAreNullOrEmptyError);
            }
            return await this.repository.AllReadonly<Car>()
                 .Where(c => c.IsActive)
                 .Where(c => c.RenterId == userId)
                 .Select(c => new CarServiceModel()
                 {
                     Address = c.Address,
                     Id = c.Id,
                     ImageUrl = c.ImageUrl,
                     PricePerMonth = c.PricePerMonth,
                     Title = c.Title,
                     IsRented = c.RenterId != null

                 })
                 .ToListAsync();
        }

        public async Task<bool> CategoryExist(int engineCategoryId)
        {
            return await this.repository.AllReadonly<EngineCategory>()
                .Where(e => e.IsActive)
                .AnyAsync(c => c.Id == engineCategoryId);
        }

        public async Task<int> Create(CarModel model, int agentId)
        {
            if (model==null)
            {
                throw new ArgumentNullException(CarConstants.ParametersAreNullOrEmptyError);
            }

            var car = new Car()
            {
                Address = model.Address,
                EngineCategoryId = model.EngineCategoryId,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PricePerMonth = model.PricePerMonth,
                Title = model.Title,
                AgentId = agentId
            };

            await this.repository.AddAsync(car);
            await this.repository.SaveChangesAsync();

            return car.Id;
        }

        public async Task DeleteCar(int carId)
        {
            var car = await this.repository.GetByIdAsync<Car>(carId);
            car.IsActive = false;
            await this.repository.SaveChangesAsync();
        }

        public async Task<int> GetCarCategoryId(int carId)
        {
            return (await this.repository.GetByIdAsync<Car>(carId)).EngineCategoryId;
        }

        public async Task<bool> HasAgentWithId(int carId, string currentUserId)
        {
            if (String.IsNullOrEmpty(currentUserId))
            {
                throw new ArgumentNullException(CarConstants.ParametersAreNullOrEmptyError);
            }

            bool result = false;

            var Car = await this.repository.AllReadonly<Car>()
                .Where(h => h.IsActive)
                .Where(h => h.Id == carId)
                .Include(h => h.Agent)
                .FirstOrDefaultAsync();

            if (Car?.Agent != null && Car.Agent.UserId == currentUserId)
            {
                result = true;
            }

            return result;
        }

        public async Task<CarDetailsModel> CarDetailsById(int id)
        {
            return await this.repository.AllReadonly<Car>()
                .Where(c => c.IsActive)
                .Where(c => c.Id == id)
                .Select(c => new CarDetailsModel()
                {
                    Id = c.Id,
                    Address = c.Address,
                    Category = c.EngineCategory.Fuel,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    IsRented = c.RenterId != null,
                    PricePerMonth = c.PricePerMonth,
                    Title = c.Title,
                    Agent = new AgentServiceModel()
                    {
                        //FullName = await this.userService.UserFullName(c.Agent.UserId),
                        Email = c.Agent.User.Email,
                        PhoneNumber = c.Agent.PhoneNumber
                    }
                })
                .FirstAsync();

            //var car = await this.repository.GetByIdAsync<Car>(id);

            //var carDetailsModel = new CarDetailsModel()
            //{
            //    Id = car.Id,
            //    Address = car.Address,
            //    Category = car.EngineCategory.Fuel,
            //    Description = car.Description,
            //    ImageUrl = car.ImageUrl,
            //    IsRented = car.RenterId != null,
            //    PricePerMonth = car.PricePerMonth,
            //    Title = car.Title,
            //    Agent = new AgentServiceModel()
            //    {
            //        Email = car.Agent.User.Email,
            //        FullName = await this.userService.UserFullName(car.Agent.UserId),
            //        PhoneNumber = car.Agent.PhoneNumber
            //    }
            //};

            //return carDetailsModel;
        }

        public async Task CarEdit(int carId, string title, string address, string description, string imageUrl, decimal pricePerMonth, int engineCategoryId)
        {
            var car = await this.repository.AllReadonly<Car>()
                .FirstOrDefaultAsync(h => h.Id == carId);

            if (car != null)
            {
                car.Title = title;
                car.Address = address;
                car.Description = description;
                car.ImageUrl = imageUrl;
                car.PricePerMonth = pricePerMonth;
                car.EngineCategoryId = engineCategoryId;
            }

            await this.repository.SaveChangesAsync();
        }

        public async Task CarEdit(int carId, CarModel carModel)
        {
            var car = await this.repository.GetByIdAsync<Car>(carId);

            car.Title = carModel.Title;
            car.Address = carModel.Address;
            car.Description = carModel.Description;
            car.ImageUrl = carModel.ImageUrl;
            car.PricePerMonth = carModel.PricePerMonth;
            car.EngineCategoryId = carModel.EngineCategoryId;

            await this.repository.SaveChangesAsync();
        }

        public async Task<bool> CarExist(int id)
        {
            return await this.repository.AllReadonly<Car>()
                .AnyAsync(h => h.Id == id && h.IsActive);
        }

        public async Task<bool> IsRented(int carId)
        {
            return (await this.repository.GetByIdAsync<Car>(carId)).RenterId != null;
        }

        public async Task<bool> IsRentedByUserWithId(int carId, string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(CarConstants.ParametersAreNullOrEmptyError);
            }
            bool result = false;

            var car = await this.repository.AllReadonly<Car>()
                .Where(h => h.IsActive)
                .Where(h => h.Id == carId)
                .Include(h => h.Agent)
                .FirstOrDefaultAsync();

            if (car != null && car.RenterId == userId)
            {
                result = true;
            }

            return result;
        }

        public async Task<IEnumerable<CarHomeModel>> LastThreeCars()
        {
            return await this.repository.AllReadonly<Car>()
                .Where(h => h.IsActive)
                .OrderByDescending(x => x.Id)
                .Select(x => new CarHomeModel()
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    Title = x.Title,
                    Address = x.Address
                })
                .Take(3)
                .ToListAsync();
        }

        public async Task LeaveCar(int carId)
        {
            var car = await this.repository.GetByIdAsync<Car>(carId);
            //this.guard.AgainstNull(car, "Car can not be found!");
            car.RenterId = null;
            await this.repository.SaveChangesAsync();
        }

        public async Task RentCar(int carId, string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(CarConstants.ParametersAreNullOrEmptyError);
            }
            var car = await this.repository.GetByIdAsync<Car>(carId);

            if (car != null && car.RenterId != null)
            {
                throw new ArgumentException("Car is already rented!");
            }
            //guard.AgainstNull(car, "Car can not be found!");
            car.RenterId = userId;

            await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarServiceModel>> GetAllExistedCar()
        {
            return await this.repository.AllReadonly<Car>()
                .Where(c => c.IsActive)
                .Select(c => new CarServiceModel()
                {
                    Id = c.Id,
                    Address = c.Address,
                    ImageUrl = c.ImageUrl,
                    IsRented = c.RenterId != null ? true : false,
                    PricePerMonth = c.PricePerMonth,
                    Title = c.Title
                })
                .ToListAsync();
        }

        
    }
}
