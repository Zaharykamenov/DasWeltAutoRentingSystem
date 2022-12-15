using CarRentingSystem.Common;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Infrastructure.Data.Models;

namespace CarRentingSystem.Core.Services
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly IRepository repository;

        public ErrorLogService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task SaveError(Exception exception, string userId)
        {
            await this.repository.AddAsync(new ErrorLog()
            {
                Message = exception.Message,
                Source = exception.Source,
                RegistedOn = DateTime.Now,
                User = userId
            });

            await this.repository.SaveChangesAsync();
        }
    }
}
