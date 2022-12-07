using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentingSystem.Infrastructure.Data.Configuration
{
    public class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.HasData(CreateAgent());
        }

        private List<Agent> CreateAgent()
        {
            var agents = new List<Agent>();

            var agent = new Agent()
            {
                Id = 1,
                PhoneNumber = "+359878883881",
                UserId = "dea12856-c198-4129-b3f3-b893d8395082"
            };

            agents.Add(agent);

            var adminAgent = new Agent()
            {
                Id = 2,
                PhoneNumber = "+359878350670",
                UserId = "bcb4f072-ecca-43c9-ab26-c060c6f364e4"
            };

            agents.Add(adminAgent);

            return agents;
        }

    }
}
