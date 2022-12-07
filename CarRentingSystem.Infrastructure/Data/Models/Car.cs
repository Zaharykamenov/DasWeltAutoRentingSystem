using CarRentingSystem.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentingSystem.Infrastructure.Data.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(CarConstants.TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(CarConstants.AddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(CarConstants.DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Precision(18, 2)]
        [Range(CarConstants.PricePerMonthMin, CarConstants.PricePerMonthMin)]
        public decimal PricePerMonth { get; set; }

        [Required]
        [ForeignKey(nameof(EngineCategory))]
        public int EngineCategoryId { get; set; }
        public EngineCategory EngineCategory { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Agent))]
        public int AgentId { get; set; }
        public Agent Agent { get; set; } = null!;

        [ForeignKey(nameof(Renter))]
        public string? RenterId { get; set; }
        public User? Renter { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
