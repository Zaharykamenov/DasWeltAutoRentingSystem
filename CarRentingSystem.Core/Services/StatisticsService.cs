using CarRentingSystem.Common;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.Statistics;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Core.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IRepository repository;

        public StatisticsService(IRepository repository)
        {
            this.repository = repository;
        }

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
