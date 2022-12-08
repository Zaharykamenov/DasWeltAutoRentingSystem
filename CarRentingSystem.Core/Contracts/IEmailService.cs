namespace CarRentingSystem.Core.Contracts
{
    public interface IEmailService
    {
        Task Send(int carId, string userId);
        Task<string> GetEmailBody(int carId, string userId);
    }
}
