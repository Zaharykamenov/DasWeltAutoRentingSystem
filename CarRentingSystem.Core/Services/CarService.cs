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
    /// <summary>
    /// This is car service class that support all operations related to cars.
    /// </summary>
    public class CarService : ICarService
    {
        /// <summary>
        /// Private properties of the class CarService
        /// </summary>
        private readonly IRepository repository;
        private readonly IGuard guard;
        private readonly IUserService userService;

        /// <summary>
        /// this is a constructor ot the class CarService with three parameters
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="guard"></param>
        /// <param name="userService"></param>
        public CarService(IRepository repository, IGuard guard, IUserService userService)
        {
            this.repository = repository;
            this.guard = guard;
            this.userService = userService;
        }

        /// <summary>
        /// This method has been used to generate CarQueryModel for the index page. It`s support searching form to perfom model by user`s parameters.
        /// There is paganation for index page about visualisation of all cars at index page.
        /// </summary>
        /// <param name="fuel"></param>
        /// <param name="searchTerm"></param>
        /// <param name="carSorting"></param>
        /// <param name="currentPage"></param>
        /// <param name="carsPerPage"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns IEnumerable of CarCategoryModel
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Return IEnumerable of string with all category names.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> AllCategoriesNames()
        {
            return await this.repository.AllReadonly<EngineCategory>()
                .Where(e => e.IsActive)
                .Select(c => c.Fuel)
                .Distinct()
                .ToListAsync();
        }

        /// <summary>
        /// Return IEnumerable of CarServiceModel that represent all cars added by current Agent. Parameters for that method is agentId.
        /// </summary>
        /// <param name="agentId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return IEnumerable of CarServiceModel that represents all cars by userId. Method has parameter userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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

        /// <summary>
        /// Method return true or false if EngineCategory exist by ID.
        /// </summary>
        /// <param name="engineCategoryId"></param>
        /// <returns></returns>
        public async Task<bool> CategoryExist(int engineCategoryId)
        {
            return await this.repository.AllReadonly<EngineCategory>()
                .Where(e => e.IsActive)
                .AnyAsync(c => c.Id == engineCategoryId);
        }

        /// <summary>
        /// Method create car in Db and return car`s ID.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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

        /// <summary>
        /// Method change car`s property isActive to false.
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        public async Task DeleteCar(int carId)
        {
            var car = await this.repository.GetByIdAsync<Car>(carId);
            car.IsActive = false;
            await this.repository.SaveChangesAsync();
        }

        /// <summary>
        /// Method return integer that represent car`s engine category ID by current car ID.
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        public async Task<int> GetCarCategoryId(int carId)
        {
            return (await this.repository.GetByIdAsync<Car>(carId)).EngineCategoryId;
        }

        /// <summary>
        /// Return true or false if there is Agent in Db with ID that added current car.
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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

        /// <summary>
        /// Return CarDetailsModel that represent car details information by car ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// This method modify car in DB
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="title"></param>
        /// <param name="address"></param>
        /// <param name="description"></param>
        /// <param name="imageUrl"></param>
        /// <param name="pricePerMonth"></param>
        /// <param name="engineCategoryId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// This method modify car in DB
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="carModel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return true or false if car exist in database by car ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> CarExist(int id)
        {
            return await this.repository.AllReadonly<Car>()
                .AnyAsync(h => h.Id == id && h.IsActive);
        }

        /// <summary>
        /// Return true or false if car is rented by some user.
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        public async Task<bool> IsRented(int carId)
        {
            return (await this.repository.GetByIdAsync<Car>(carId)).RenterId != null;
        }

        /// <summary>
        /// Return true or false if car is rented by current user ID.
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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

        /// <summary>
        /// Return IEnumerable of CarHomeModel that represent last three added cars in database.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Method leave car by car ID.
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        public async Task LeaveCar(int carId)
        {
            var car = await this.repository.GetByIdAsync<Car>(carId);
            //this.guard.AgainstNull(car, "Car can not be found!");
            car.RenterId = null;
            await this.repository.SaveChangesAsync();
        }

        /// <summary>
        /// Method rent a car by user.
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
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

        /// <summary>
        /// Return IEnumerable of CarServiceModel that represent all existing car in database.
        /// </summary>
        /// <returns></returns>
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
