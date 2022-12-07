using CarRentingSystem.Core.Contracts;

namespace CarRentingSystem.Core.Models.Car
{
    public class CarHomeModel : ICarModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string Address { get; init; } = null!;
    }
}
