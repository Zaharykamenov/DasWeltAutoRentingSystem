using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentingSystem.Infrastructure.Data.Configuration
{
    internal class TransportConfiguration : IEntityTypeConfiguration<Transport>
    {
        public void Configure(EntityTypeBuilder<Transport> builder)
        {
            builder.HasData(CreateTransports());
        }

        private List<Transport> CreateTransports()
        {
            return new List<Transport>()
            {
                new Transport()
                {
                    Id = 1,
                    CompanyName="VidaTrans Ltd",
                    DeliveryDays=5,
                    ImageUrl="https://www.vida.se/wp-content/uploads/2018/12/vida_rgb.jpg",
                    Description = "Best transport company in the west Bulgaria!",
                    IsActive=true,
                    AgentId=1,
                    PricePerKm = 2.0m
                },
                new Transport()
                {
                    Id=2,
                    CompanyName="SofiaTrans Ltd",
                    DeliveryDays = 3,
                    ImageUrl="https://d1yjjnpx0p53s8.cloudfront.net/styles/logo-thumbnail/s3/102017/untitled-3_77.png?Q07xLRtpQwmjEjwxEl7zei_mYpcEEbdE&itok=y1qvbWxC",
                    Description = "The best transport company in central Bulgaria!",
                    IsActive = true,
                    AgentId = 2,
                    PricePerKm = 10.0m
                },
                new Transport()
                {
                    Id=3,
                    CompanyName="Speedy Ltd",
                    DeliveryDays = 1,
                    ImageUrl = "https://www.speedy.bg/uploads/file_manager_uploads/Pics/speedy-logo-4c--cmyk.jpg",
                    Description = "The biggest and the fastest transport company in Bulgaria!",
                    IsActive = true,
                    AgentId=1,
                    PricePerKm = 20.0m
                }
            };
        }
    }
}
