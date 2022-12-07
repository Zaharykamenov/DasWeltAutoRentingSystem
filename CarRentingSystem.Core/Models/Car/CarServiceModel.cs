using CarRentingSystem.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace CarRentingSystem.Core.Models.Car
{
    public class CarServiceModel : ICarModel
    {
        public int Id { get; init; }
        public string Title { get; init; } = null!;
        public string Address { get; init; } = null!;

        [Display(Name = "ImageUrl URL")]
        public string ImageUrl { get; init; } = null!;

        [Display(Name = "Price Per Month")]
        public decimal PricePerMonth { get; init; }

        [Display(Name = "Is Rented")]
        public bool IsRented { get; init; }
    }
}
