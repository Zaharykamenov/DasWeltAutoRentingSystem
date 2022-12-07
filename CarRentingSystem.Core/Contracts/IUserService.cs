using CarRentingSystem.Core.Models.User;

namespace CarRentingSystem.Core.Contracts
{
    public interface IUserService
    {
        Task<string> UserFullName(string userId);
        Task<IEnumerable<UserServiceModel>> All();
        Task<bool> UserHasRents(string userId);
    }
}
