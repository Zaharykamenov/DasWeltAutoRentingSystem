using CarRentingSystem.Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CarRentingSystem.Core.Models.Transport
{
    public class AddTransportModel
    {
        public int Id { get; set; }

        [Display(Name = "Company")]
        [Required]
        [StringLength(TransportConstants.CompanyNameMaxLength, MinimumLength = TransportConstants.CompanyNameMinLength)]
        public string CompanyName { get; set; } = null!;

        [Display(Name = "Image")]
        [Required]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Description")]
        [Required]
        [StringLength(TransportConstants.DescriptionMaxLength, MinimumLength = TransportConstants.DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Display(Name = "Price per km")]
        [Required]
        [Precision(18, 2)]
        [Range(TransportConstants.PricePerMonthMin, TransportConstants.PricePerMonthMax)]
        public decimal PricePerKm { get; set; }

        [Display(Name = "Days for Delivery")]
        [Required]
        [Range(TransportConstants.DeliveryDaysMin, TransportConstants.DeliveryDaysMax)]
        public int DeliveryDays { get; set; }
    }
}
