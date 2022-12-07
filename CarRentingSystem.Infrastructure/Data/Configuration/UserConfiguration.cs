using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentingSystem.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(CreateUsers());
        }

        private List<User> CreateUsers()
        {
            var users = new List<User>();
            var hasher = new PasswordHasher<User>();

            var agentUser = new User()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "zaharykamenov@gmail.com",
                NormalizedUserName = "ZAHARYKAMENOV@GMAIL.COM",
                Email = "zaharykamenov@gmail.com",
                NormalizedEmail = "zaharykamenov@gmail.com",
                FirstName = "Zahary",
                LastName = "Kamenov"
            };

            agentUser.PasswordHash =
                 hasher.HashPassword(agentUser, "@gent1234");

            users.Add(agentUser);

            var guestUser = new User()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "guest@mail.com",
                NormalizedUserName = "guest@mail.com",
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com",
                FirstName = "Guest",
                LastName = "Guestov"
            };

            guestUser.PasswordHash =
            hasher.HashPassword(guestUser, "guest#1234");

            users.Add(guestUser);

            var adminUser = new User()
            {
                Id = "bcb4f072-ecca-43c9-ab26-c060c6f364e4",
                UserName = "admin@mail.com",
                NormalizedUserName = "admin@mail.com",
                Email = "admin@mail.com",
                NormalizedEmail = "admin@mail.com",
                FirstName = "Admin",
                LastName = "Adminov"
            };

            adminUser.PasswordHash =
            hasher.HashPassword(adminUser, "admin#1234");

            users.Add(adminUser);

            return users;
        }

    }
}
