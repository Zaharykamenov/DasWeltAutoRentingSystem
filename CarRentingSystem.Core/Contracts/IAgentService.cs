namespace CarRentingSystem.Core.Contracts
{
    public interface IAgentService
    {
        Task<bool> ExistById(string userId);

        Task<bool> AgentWithPhoneNumberExist(string phoneNumber);

        Task Create(string userId, string phoneNumber);

        Task<int> GetAgentId(string userId);
    }
}
