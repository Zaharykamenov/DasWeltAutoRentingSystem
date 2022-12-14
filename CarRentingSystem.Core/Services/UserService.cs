using CarRentingSystem.Common;
using CarRentingSystem.Core.Constants;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.User;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CarRentingSystem.Core.Services
{
    /// <summary>
    /// Class that represent all method related with user
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// private property repository which is DB connection
        /// </summary>
        private readonly IRepository repository;

        /// <summary>
        /// Constructor of class UserService
        /// </summary>
        /// <param name="repository"></param>
        public UserService(IRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Method that get all users with agent includede
        /// </summary>
        /// <returns>IEnumerable of UserServiceModel</returns>
        public async Task<IEnumerable<UserServiceModel>> All()
        {
            var allUsers = new List<UserServiceModel>();

            var agents = await this.repository.AllReadonly<Agent>()
                .Include(a => a.User)
                .Select(a => new UserServiceModel()
                {
                    Email = a.User.Email,
                    FullName = $"{a.User.FirstName} {a.User.LastName}",
                    PhoneNumber = a.PhoneNumber
                })
                .ToListAsync();

            allUsers.AddRange(agents);

            var users = await this.repository.AllReadonly<User>()
                .Where(u => !this.repository.AllReadonly<Agent>().Any(a => a.UserId == u.Id))
                .Select(u => new UserServiceModel()
                {
                    Email = u.Email,
                    FullName = $"{u.FirstName} {u.LastName}",
                    PhoneNumber = u.PhoneNumber
                })
                .ToListAsync();

            allUsers.AddRange(users);

            return allUsers;
        }

        /// <summary>
        /// Method that get full name of specific user ID.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>string</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<string> UserFullName(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(UserConstants.ParametersAreNullOrEmptyError);
            }
            StringBuilder sb = new StringBuilder();

            var user = await this.repository.GetByIdAsync<User>(userId); //.AllReadonly<User>().FirstOrDefault(u=>u.Id==userId);

            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
            {
                return String.Empty;
            }

            sb.Append($"{user.FirstName} {user.LastName}");
            return sb.ToString().TrimEnd();
        }

        /// <summary>
        /// Method that check of specific user has rents 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> UserHasRents(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(UserConstants.ParametersAreNullOrEmptyError);
            }
            return await this.repository.All<Car>()
                .AnyAsync(h => h.RenterId == userId);
        }
    }
}
