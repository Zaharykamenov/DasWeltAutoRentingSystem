using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentingSystem.Infrastructure.Data.Configuration
{
    internal class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasData(CreateCars());
        }

        private List<Car> CreateCars()
        {
            return new List<Car>()
            {
                new Car()
            {
                Id = 1,
                Title = "VW Tiguan R-Line 2.0 TDI SCR 4MOTION DSG ",
                Address = " AutoChoice 5002 Велико Търново.",
                Description = "Тестов автомобил на Авточойс ООД. Заводска гаранция до 08.2027г. ",
                ImageUrl = "https://d3t2kd17lko26w.cloudfront.net/CD05CE68E99EDC4DFA5D4FA3B17579C5/images/09fa6163-bc88-44bd-b560-d3b180d7a75b/webp/768",
                PricePerMonth = 8492.00M,
                EngineCategoryId = 1,
                AgentId = 1,
                RenterId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"
            },
                new Car()
            {
                Id = 2,
                Title = "VW Tiguan",
                Address = "Порше София Юг кв. Овча купел, ул. Бойчо Бойчев №16, София",
                Description = "Клиентски автомобил. Закупуван е през м.Юни 2018 г. от Порше БГ.",
                ImageUrl = "https://d3t2kd17lko26w.cloudfront.net/973025CA64EDE94792AFBD2A58563F56/images/e03baf61-236b-43c1-9fd3-fd4c04f61757/webp/1440",
                PricePerMonth = 5575.00M,
                EngineCategoryId = 2,
                AgentId = 1
            },
                new Car()
            {
                Id = 3,
                Title = "VW ID.3",
                Address = "Autotrade, бул.Трети Март №59, Варна",
                Description = "Volkswagen ID3/ 58kwh в отлично състояние. Автомобилът се продава с фабрична гаранция до 12. 2025г. или пробег от 150 000км.",
                ImageUrl = "https://d3t2kd17lko26w.cloudfront.net/76368CA5B4D99321B355F7244F124509/images/74394fa8-06c0-4353-93a6-4872fe99a251/webp/768",
                PricePerMonth = 6833.00M,
                EngineCategoryId = 3,
                AgentId = 1
            },
            new Car()
            {
                Id = 4,
                Title = "VW Golf",
                Address = "HAAS-60 Бул.България 121, Пловдив",
                Description = "Автомобилът е киентски, закупен от Volkswagen Лизинг Германия и се предлага с 1 година или 20000км DasWeltAuto гаранция. Възможност за 5 годишен лизинг, регистриран N1!",
                ImageUrl = "https://d3t2kd17lko26w.cloudfront.net/E45A3975D6BF895090CBD1EF1F78B217/images/0a26af68-26d0-43df-a7fb-9975e49f3c92/webp/768",
                PricePerMonth = 4825.00M,
                EngineCategoryId = 4,
                AgentId = 1
            }
        };
        }
    }
}
