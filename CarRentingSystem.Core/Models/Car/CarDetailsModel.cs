using CarRentingSystem.Core.Models.Agent;

namespace CarRentingSystem.Core.Models.Car
{
    public class CarDetailsModel : CarServiceModel
    {
        public string Description { get; set; } = null!;
        public string Category { get; set; } = null!;
        public AgentServiceModel Agent { get; set; } = null!;
    }
}
