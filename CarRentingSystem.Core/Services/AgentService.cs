using CarRentingSystem.Common;
using CarRentingSystem.Core.Constants;
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
            if (String.IsNullOrEmpty(userId) || String.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentNullException(AgentConstants.ParametersAreNullOrEmptyError);
            }

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
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(AgentConstants.ParametersAreNullOrEmptyError);
            }
            return await this.repository.All<Agent>()
                .AnyAsync(a => a.UserId == userId);
        }

        public async Task<int> GetAgentId(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(AgentConstants.ParametersAreNullOrEmptyError);
            }

            return (await this.repository.AllReadonly<Agent>()
                .FirstOrDefaultAsync(a => a.UserId == userId))?.Id ?? 0;
        }

        public async Task<bool> AgentWithPhoneNumberExist(string phoneNumber)
        {
            if (String.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentNullException(AgentConstants.ParametersAreNullOrEmptyError);
            }
            return await this.repository.All<Agent>()
                .AnyAsync(a => a.PhoneNumber == phoneNumber);
        }
    }
}
