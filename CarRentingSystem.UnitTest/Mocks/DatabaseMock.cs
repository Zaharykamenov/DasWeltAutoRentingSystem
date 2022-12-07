using CarRentingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.UnitTest.Mocks
{
    public static class DatabaseMock
    {
        public static ApplicationDbContext Instance
        {
            get 
            {
                var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase("ApplicationDbContextInMemory" + DateTime.Now.Ticks.ToString())
                    .Options;

                return new ApplicationDbContext(dbContextOptions, false);
            }
        }
    }
}
