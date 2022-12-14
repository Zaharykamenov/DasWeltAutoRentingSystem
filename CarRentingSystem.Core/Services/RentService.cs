using CarRentingSystem.Common;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.Rent;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Core.Services
{
    /// <summary>
    /// Class that represent all related methods with rent car.
    /// </summary>
    public class RentService : IRentService
    {
        private readonly IRepository repository;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="repository"></param>
        public RentService(IRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Method get all cars from database with user and agent information
        /// </summary>
        /// <returns>IEnumerable of RentServiceModel</returns>
        public async Task<IEnumerable<RentServiceModel>> All()
        {
            return await this.repository.All<Car>()
                .Include(c => c.Agent.User)
                .Include(c => c.Renter)
                .Where(c => c.RenterId != null)
                .Select(c => new RentServiceModel()
                {
                    AgentEmail = c.Agent.User.Email,
                    AgentFullName = $"{c.Agent.User.FirstName} {c.Agent.User.LastName}",
                    CarImageUrl = c.ImageUrl,
                    CarTitle = c.Title,
                    RenterEmail = c.Renter != null ? c.Renter.Email : String.Empty,
                    RenterFullName = c.Renter != null ? $"{c.Renter.FirstName} {c.Renter.LastName}" : String.Empty,
                    AgentPhoneNumber = c.Agent.PhoneNumber
                })
                .ToListAsync();
        }
    }
}
