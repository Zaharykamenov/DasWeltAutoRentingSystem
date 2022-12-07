using CarRentingSystem.Common;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Core.Services
{
    public class AgentService : IAgentService
    {
        private readonly IRepository repository;

        public AgentService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task Create(string userId, string phoneNumber)
        {
            var agent = new Agent()
            {
                UserId = userId,
                PhoneNumber = phoneNumber
            };        

            await this.repository.AddAsync(agent);
            await this.repository.SaveChangesAsync();
        }

        public async Task<bool> ExistById(string userId)
        {
            return await this.repository.All<Agent>()
                .AnyAsync(a => a.UserId == userId);
        }

        public async Task<int> GetAgentId(string userId)
        {
            return (await this.repository.AllReadonly<Agent>()
                .FirstOrDefaultAsync(a => a.UserId == userId))?.Id ?? 0;
        }

        

        public async Task<bool> AgentWithPhoneNumberExist(string phoneNumber)
        {
            return await this.repository.All<Agent>()
                .AnyAsync(a => a.PhoneNumber == phoneNumber);
        }
    }
}
