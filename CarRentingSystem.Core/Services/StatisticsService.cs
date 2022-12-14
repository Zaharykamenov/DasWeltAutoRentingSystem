using CarRentingSystem.Common;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.Statistics;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Core.Services
{
    /// <summary>
    /// Class represent all method related with statistics information
    /// </summary>
    public class StatisticsService : IStatisticsService
    {
        /// <summary>
        /// private property repository
        /// </summary>
        private readonly IRepository repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public StatisticsService(IRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Method that count all active cars in database and all rented cars.
        /// </summary>
        /// <returns>StatisticsServiceModel represent all active car and all rented cars</returns>
        public async Task<StatisticsServiceModel> Total()
        {
            int totalCars = await this.repository.AllReadonly<Car>()
                .CountAsync(c => c.IsActive);

            int rentedCars = await this.repository.AllReadonly<Car>()
                .CountAsync(c => c.IsActive && c.RenterId != null);

            return new StatisticsServiceModel()
            {
                TotalCars = totalCars,
                TotalRents = rentedCars
            };
        }
    }
}
