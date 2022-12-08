using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentingSystem.Core.Models.EngineCategory
{
    public class EngineCategoryCreateModel
    {
        public string Fuel { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
