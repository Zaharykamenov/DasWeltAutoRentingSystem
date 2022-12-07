using System.ComponentModel.DataAnnotations;

namespace CarRentingSystem.Core.Models.EngineCategory
{
    public class EngineCategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Fuel Type")] 
        public string Fuel { get; set; } = null!;

        [Display(Name = "Description")] 
        public string Description { get; set; } = null!;

        [Display(Name = "Cars")]
        public int Cars { get; set; }

    }
}
