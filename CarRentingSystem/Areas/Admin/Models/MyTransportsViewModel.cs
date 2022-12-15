using CarRentingSystem.Core.Models.Car;
using CarRentingSystem.Core.Models.Transport;

namespace CarRentingSystem.Areas.Admin.Models
{
    public class MyTransportsViewModel
    {
        public IEnumerable<AllTransportsViewModel> AddedTransports { get; set; } = new List<AllTransportsViewModel>();

        public IEnumerable<AllTransportsViewModel> RentedTransports { get; set; } = new List<AllTransportsViewModel>();
    }
}
