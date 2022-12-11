using CarRentingSystem.Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CarRentingSystem.Core.Models.Transport
{
    public class TransportEditModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TransportConstants.CompanyNameMaxLength, MinimumLength = TransportConstants.CompanyNameMinLength)]
        public string CompanyName { get; set; } = null!;

        [Required]
        [StringLength(TransportConstants.DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Precision(18, 2)]
        [Range(TransportConstants.PricePerMonthMin, TransportConstants.PricePerMonthMin)]
        public decimal PricePerKm { get; set; }

        [Required]
        [Range(TransportConstants.DeliveryDaysMin, TransportConstants.DeliveryDaysMax)]
        public int DeliveryDays { get; set; }
    }
}
