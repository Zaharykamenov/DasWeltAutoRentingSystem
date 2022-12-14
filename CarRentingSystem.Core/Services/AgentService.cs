using CarRentingSystem.Common;
using CarRentingSystem.Core.Constants;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Core.Services
{
    /// <summary>
    /// Agent service
    /// </summary>
    public class AgentService : IAgentService
    {
        /// <summary>
        ///         private readonly IRepository repository;
        /// </summary>
        private readonly IRepository repository;

        /// <summary>
        /// This is constructor of the class
        /// </summary>
        /// <param name="repository"></param>
        public AgentService(IRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// This method Create Agent, then add to DB and save it to DB by using parameters below.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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

        /// <summary>
        /// This method check if agent exist in Db by userId and return true or false.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> ExistById(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(AgentConstants.ParametersAreNullOrEmptyError);
            }
            return await this.repository.All<Agent>()
                .AnyAsync(a => a.UserId == userId);
        }

        /// <summary>
        /// This method return agentId by userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<int> GetAgentId(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(AgentConstants.ParametersAreNullOrEmptyError);
            }

            return (await this.repository.AllReadonly<Agent>()
                .FirstOrDefaultAsync(a => a.UserId == userId))?.Id ?? 0;
        }

        /// <summary>
        /// This method is checks if there is an agent with a given phone number.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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
