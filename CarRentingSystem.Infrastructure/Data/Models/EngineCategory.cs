using System.ComponentModel.DataAnnotations;
using CarRentingSystem.Infrastructure.Constants;

namespace CarRentingSystem.Infrastructure.Data.Models
{
    public class EngineCategory
    {
        public EngineCategory()
        {
            Cars = new List<Car>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(EngineCategoryConstants.FuelMaxLength)]
        public string Fuel { get; set; } = null!;

        [Required]
        [StringLength(EngineCategoryConstants.DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public ICollection<Car> Cars { get; set; }
    }
}
