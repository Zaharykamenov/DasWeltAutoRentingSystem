using CarRentingSystem.Infrastructure.Data.Configuration;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        private bool seedDb;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, bool seedDb = true)
            : base(options)
        {
            if (this.Database.IsRelational())
            {
                this.Database.Migrate();
            }
            else
            {
                this.Database.EnsureCreated();
            }

            this.seedDb = seedDb;
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<EngineCategory> EngineCategory { get; set; }
        public virtual DbSet<Agent> Agents { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (this.seedDb)
            {
                builder.ApplyConfiguration(new UserConfiguration());
                builder.ApplyConfiguration(new AgentConfiguration());
                builder.ApplyConfiguration(new EngineCategoryConfiguration());
                builder.ApplyConfiguration(new CarConfiguration());
            }

            base.OnModelCreating(builder);
        }
    }
}