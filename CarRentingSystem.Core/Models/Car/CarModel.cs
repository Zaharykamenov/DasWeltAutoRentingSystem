using CarRentingSystem.Core.Constants;
using CarRentingSystem.Core.Contracts;
using System.ComponentModel.DataAnnotations;


namespace CarRentingSystem.Core.Models.Car
{
    public class CarModel : ICarModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(CarConstants.TitleMaxLength, MinimumLength = CarConstants.TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(CarConstants.AddressMaxLength, MinimumLength = CarConstants.AddressMinLength)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(CarConstants.DescriptionMaxLength, MinimumLength = CarConstants.DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Display(Name = "Price per month")]
        [Range(CarConstants.PricePerMonthMin, maximum: CarConstants.PricePerMonthMax, ErrorMessage = ErrorConstants.ErrorPricePerMonth)]
        public decimal PricePerMonth { get; set; }

        [Display(Name = "Category")]
        public int EngineCategoryId { get; set; }

        public IEnumerable<CarCategoryModel> CarCategories { get; set; } = new List<CarCategoryModel>();




    }
}
