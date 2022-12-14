using CarRentingSystem.Common;
using CarRentingSystem.Core.Constants;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.Agent;
using CarRentingSystem.Core.Models.Transport;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Core.Services
{
    /// <summary>
    /// Class that represent all method for transport.
    /// </summary>
    public class TransportService : ITransportService
    {
        /// <summary>
        /// Private property of repository
        /// </summary>
        private readonly IRepository repository;

        /// <summary>
        /// Constructor of class transport service
        /// </summary>
        /// <param name="repository"></param>
        public TransportService(IRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Method that get all active transports from database
        /// </summary>
        /// <returns>IEnumerable of AllTransportsViewModel</returns>
        public async Task<IEnumerable<AllTransportsViewModel>> GetAllTransports()
        {
            return await this.repository.AllReadonly<Transport>()
                .Where(t => t.IsActive)
                .Select(t => new AllTransportsViewModel()
                {
                    Id = t.Id,
                    CompanyName = t.CompanyName,
                    DeliveryDays = t.DeliveryDays,
                    PricePerKm = t.PricePerKm,
                    Description = t.Description,
                    ImageUrl = t.ImageUrl,
                    IsRented = t.RenterId != null ? true : false,
                    Agent = new AgentServiceModel()
                    {
                        Email = t.Agent.User.Email,
                        FullName = $"{t.Agent.User.FirstName} {t.Agent.User.LastName}",
                        PhoneNumber = t.Agent.PhoneNumber
                    }
                })
                .ToListAsync();
        }

        /// <summary>
        /// Return true or false if transport is rented by current userID.
        /// </summary>
        /// <param name="transportId"></param>
        /// <param name="userId"></param>
        /// <returns>bool</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> IsRentedByUserWithId(int transportId, string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(TransportConstants.ParametersAreNullOrEmptyError);
            }
            bool result = false;

            var transport = await this.repository.AllReadonly<Transport>()
                .Where(t => t.IsActive)
                .Where(t => t.Id == transportId)
                .Include(t => t.Renter)
                .FirstOrDefaultAsync();

            if (transport != null && transport.RenterId == userId)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Method that represent renting current transport by userID.
        /// </summary>
        /// <param name="transportId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task RentTransport(int transportId, string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(TransportConstants.ParametersAreNullOrEmptyError);
            }
            var transport = await this.repository.GetByIdAsync<Transport>(transportId);

            if (transport != null && transport.RenterId != null)
            {
                throw new ArgumentException("Transport is already rented!");
            }
            //guard.AgainstNull(car, "Car can not be found!");
            if (transport == null)
            {
                throw new InvalidOperationException("Transport was not found!");
            }

            transport.RenterId = userId;

            await this.repository.SaveChangesAsync();
        }

        /// <summary>
        /// Method that leave transport which is rented by current user.
        /// </summary>
        /// <param name="transportId"></param>
        /// <returns></returns>
        public async Task LeaveTransport(int transportId)
        {
            var transport = await this.repository.GetByIdAsync<Transport>(transportId);

            if (transport != null)
            {
                transport.RenterId = null;

                await this.repository.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Method checks that transport exist in database by ID
        /// </summary>
        /// <param name="transportId"></param>
        /// <returns>Return true or false</returns>
        public async Task<bool> TransportExist(int transportId)
        {
            return await this.repository.AllReadonly<Transport>()
                .AnyAsync(h => h.Id == transportId && h.IsActive);
        }

        /// <summary>
        /// Method checks that transport is rented.
        /// </summary>
        /// <param name="transportId"></param>
        /// <returns>Return true or false</returns>
        public async Task<bool> IsRented(int transportId)
        {
            return (await this.repository.GetByIdAsync<Transport>(transportId)).RenterId != null ? true : false;
        }

        /// <summary>
        /// Method get all active transport by user ID. 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>IEnumerable of AllTransportsViewModel</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<IEnumerable<AllTransportsViewModel>> AllTransportsByUserId(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(TransportConstants.ParametersAreNullOrEmptyError);
            }
            return await this.repository.AllReadonly<Transport>()
                 .Where(t => t.IsActive)
                 .Where(t => t.RenterId == userId)
                 .Select(t => new AllTransportsViewModel()
                 {
                     Id = t.Id,
                     ImageUrl = t.ImageUrl,
                     PricePerKm = t.PricePerKm,
                     CompanyName = t.CompanyName,
                     DeliveryDays = t.DeliveryDays,
                     Description = t.Description,
                     IsRented = t.RenterId != null
                 })
                 .ToListAsync();
        }

        /// <summary>
        /// Method that get transport details information by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AllTransportsViewModel</returns>
        public async Task<AllTransportsViewModel> TransportDetailsById(int id)
        {
            return await this.repository.AllReadonly<Transport>()
                .Where(c => c.IsActive)
                .Where(c => c.Id == id)
                .Select(c => new AllTransportsViewModel()
                {
                    Id = c.Id,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    IsRented = c.RenterId != null,
                    PricePerKm = c.PricePerKm,
                    CompanyName = c.CompanyName,
                    DeliveryDays = c.DeliveryDays,
                    Agent = new AgentServiceModel()
                    {
                        FullName = $"{c.Agent.User.FirstName} {c.Agent.User.LastName}",
                        Email = c.Agent.User.Email,
                        PhoneNumber = c.Agent.PhoneNumber
                    }
                })
                .FirstAsync();
        }

        /// <summary>
        /// Method check if transport has been added by agent.
        /// </summary>
        /// <param name="transportId"></param>
        /// <param name="currentUserId"></param>
        /// <returns>Return true or false</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> HasAgentWithId(int transportId, string currentUserId)
        {
            if (String.IsNullOrEmpty(currentUserId))
            {
                throw new ArgumentNullException(TransportConstants.ParametersAreNullOrEmptyError);
            }
            bool result = false;

            var transport = await this.repository.AllReadonly<Transport>()
                .Where(t => t.IsActive)
                .Where(t => t.Id == transportId)
                .Include(t => t.Agent)
                .FirstOrDefaultAsync();

            if (transport?.Agent != null && transport.Agent.UserId == currentUserId)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Method that check if user has rented car.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Return true or false</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> HasRentedCarByUserId(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(TransportConstants.ParametersAreNullOrEmptyError);
            }
            bool hasRentedCars = false;

            var cars = await this.repository.AllReadonly<Car>()
                .Where(c => c.RenterId == userId)
                .ToListAsync();

            if (cars.Any())
            {
                hasRentedCars = true;
            }

            return hasRentedCars;
        }

        /// <summary>
        /// Method add transport to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public AddTransportModel AddTransportModel(AllTransportsViewModel model)
        {
            if (model==null)
            {
                throw new ArgumentNullException(TransportConstants.ParametersAreNullOrEmptyError);
            }
            return new AddTransportModel()
            {
                Id=model.Id,
                CompanyName=model.CompanyName,
                DeliveryDays=model.DeliveryDays,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PricePerKm = model.PricePerKm
            };
        }

        /// <summary>
        /// Method that modify specific transport in database.
        /// </summary>
        /// <param name="transportId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task TransportEdit(int transportId, AddTransportModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(TransportConstants.ParametersAreNullOrEmptyError);
            }
            var transport = await this.repository.GetByIdAsync<Transport>(transportId);

            transport.CompanyName = model.CompanyName;
            transport.DeliveryDays = model.DeliveryDays;
            transport.Description = model.Description;
            transport.ImageUrl = model.ImageUrl;
            transport.PricePerKm = model.PricePerKm;

            await this.repository.SaveChangesAsync();
        }

        /// <summary>
        /// Method that make transport property is active to false.
        /// </summary>
        /// <param name="transportId"></param>
        /// <returns></returns>
        public async Task DeleteTransport(int transportId)
        {
            var transport = await this.repository.GetByIdAsync<Transport>(transportId);
            transport.IsActive = false;
            await this.repository.SaveChangesAsync();
        }

        /// <summary>
        /// Method that create transport in database
        /// </summary>
        /// <param name="model"></param>
        /// <param name="agentId"></param>
        /// <returns>Retirn integer that represent new created transport ID</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<int> Create(AddTransportModel model, int agentId)
        {
            if (model == null)
            {
                throw new ArgumentNullException(TransportConstants.ParametersAreNullOrEmptyError);
            }
            var transport = new Transport()
            {
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PricePerKm = model.PricePerKm,
                CompanyName = model.CompanyName,
                DeliveryDays = model.DeliveryDays,
                AgentId = agentId
            };

            await this.repository.AddAsync(transport);
            await this.repository.SaveChangesAsync();

            return transport.Id;
        }

        /// <summary>
        /// Method that get all transport uploaded by specific agent ID.
        /// </summary>
        /// <param name="agentId"></param>
        /// <returns>IEnumerable of AllTransportsViewModel</returns>
        public async Task<IEnumerable<AllTransportsViewModel>> AllTransportsByAgentId(int agentId)
        {
            return await this.repository.AllReadonly<Transport>()
                 .Where(t => t.IsActive)
                 .Where(t => t.AgentId == agentId)
                 .Select(t => new AllTransportsViewModel()
                 {
                     Id = t.Id,
                     ImageUrl = t.ImageUrl,
                     PricePerKm = t.PricePerKm,
                     CompanyName = t.CompanyName,
                     DeliveryDays = t.DeliveryDays,
                     Description = t.Description,
                     IsRented = t.RenterId != null
                 })
                 .ToListAsync();
        }
    }
}
