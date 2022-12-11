using CarRentingSystem.Core.Models.Transport;

namespace CarRentingSystem.Core.Contracts
{
    public interface ITransportService
    {
        Task<IEnumerable<AllTransportsViewModel>> GetAllTransports();
        Task<bool> IsRentedByUserWithId(int transportId, string userId);
        Task RentTransport(int transportId, string userId);
        Task LeaveTransport(int transportId);
        Task<bool> TransportExist(int transportId);
        Task<bool> IsRented(int transportId);
        Task<IEnumerable<AllTransportsViewModel>> AllTransportsByUserId(string userId);
        Task<AllTransportsViewModel> TransportDetailsById(int id);
        AddTransportModel AddTransportModel(AllTransportsViewModel model);
        Task<bool> HasAgentWithId(int transportId, string currentUserId);
        Task<bool> HasRentedCarByUserId(string userId);
        Task TransportEdit(int transportId, AddTransportModel model);
        Task DeleteTransport(int transportId);
        Task<int> Create(AddTransportModel model, int agentId);
        Task<IEnumerable<AllTransportsViewModel>> AllTransportsByAgentId(int agentId);
    }
}
