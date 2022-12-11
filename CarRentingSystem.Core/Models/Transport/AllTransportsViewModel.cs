using System.ComponentModel.DataAnnotations;
using CarRentingSystem.Core.Models.Agent;
using CarRentingSystem.Infrastructure.Data.Constants;

namespace CarRentingSystem.Core.Models.Transport
{
    public class AllTransportsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Company")]
        public string CompanyName { get; set; } = null!;

        [Display(Name = "Image")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Description")]
        public string Description { get; set; } = null!;

        [Display(Name = "Price per km")]
        public decimal PricePerKm { get; set; }

        [Display(Name = "Days for Delivery")]
        public int DeliveryDays { get; set; }

        [Display(Name = "Contacts")]
        public AgentServiceModel Agent { get; set; }

        [Display(Name = "Is Rented")]
        public bool IsRented { get; init; }
    }
}
