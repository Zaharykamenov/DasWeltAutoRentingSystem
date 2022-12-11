using CarRentingSystem.Infrastructure.Constants;
using CarRentingSystem.Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentingSystem.Infrastructure.Data.Models
{
    public class Transport
    {
        [Key]
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

        [Required]
        [ForeignKey(nameof(Agent))]
        public int AgentId { get; set; }
        public Agent Agent { get; set; } = null!;

        [ForeignKey(nameof(Renter))]
        public string? RenterId { get; set; }
        public User? Renter { get; set; }

        [Required]
        public bool IsActive { get; set; }

    }
}
