namespace CarRentingSystem.Core.Models.Car
{
    public class CarQueryModel
    {
        public int TotalCarsCount { get; set; }

        public IEnumerable<CarServiceModel> Cars { get; set; } = new List<CarServiceModel>();
    }
}
