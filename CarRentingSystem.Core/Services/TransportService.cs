using CarRentingSystem.Common;
using CarRentingSystem.Core.Constants;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.Agent;
using CarRentingSystem.Core.Models.Transport;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Core.Services
{
    public class TransportService : ITransportService
    {
        private readonly IRepository repository;

        public TransportService(IRepository repository)
        {
            this.repository = repository;
        }

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

        public async Task LeaveTransport(int transportId)
        {
            var transport = await this.repository.GetByIdAsync<Transport>(transportId);

            if (transport != null)
            {
                transport.RenterId = null;

                await this.repository.SaveChangesAsync();
            }
        }

        public async Task<bool> TransportExist(int transportId)
        {
            return await this.repository.AllReadonly<Transport>()
                .AnyAsync(h => h.Id == transportId && h.IsActive);
        }

        public async Task<bool> IsRented(int transportId)
        {
            return (await this.repository.GetByIdAsync<Transport>(transportId)).RenterId != null ? true : false;
        }

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

        public async Task DeleteTransport(int transportId)
        {
            var transport = await this.repository.GetByIdAsync<Transport>(transportId);
            transport.IsActive = false;
            await this.repository.SaveChangesAsync();
        }

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
