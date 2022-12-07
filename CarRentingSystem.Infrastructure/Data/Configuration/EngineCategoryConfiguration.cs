using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CarRentingSystem.Infrastructure.Data.Configuration
{
    public class EngineCategoryConfiguration : IEntityTypeConfiguration<EngineCategory>
    {
        public void Configure(EntityTypeBuilder<EngineCategory> builder)
        {
            builder.HasData(CreateCategories());
        }

        private List<EngineCategory> CreateCategories()
        {
            var categories = new List<EngineCategory>()
            {
                new EngineCategory()
                {
                    Id = 1,
                    Description = "2.0 TDI 147 kW/200 K.C",
                    Fuel = "Дизел"
                },
                new EngineCategory()
                {
                    Id = 2,
                    Description = "2.0TSI 4MOTION BMT 132 kW/180 K.C",
                    Fuel = "Бензин"
                },
                new EngineCategory()
                {
                    Id = 3,
                    Description = "ProP RWD 58 kWh,150кВт/204к.с./1-ст",
                    Fuel = "Електро"
                },
                new EngineCategory()
                {
                    Id = 4,
                    Description = "e-Golf PSM 100 kW/136 K.C",
                    Fuel = "Хибрид"
                }
            };
            return categories;
        }
    }
}
