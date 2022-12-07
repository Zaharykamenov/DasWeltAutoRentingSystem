using CarRentingSystem.Common;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.User;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CarRentingSystem.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;
        public UserService(IRepository repository)
        {
            this.repository = repository;
        }

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

        public async Task<string> UserFullName(string userId)
        {
            StringBuilder sb = new StringBuilder();

            var user = await this.repository.GetByIdAsync<User>(userId); //.AllReadonly<User>().FirstOrDefault(u=>u.Id==userId);

            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
            {
                return String.Empty;
            }

            sb.Append($"{user.FirstName} {user.LastName}");
            return sb.ToString().TrimEnd();
        }

        public async Task<bool> UserHasRents(string userId)
        {
            return await this.repository.All<Car>()
                .AnyAsync(h => h.RenterId == userId);
        }
    }
}
