using CarRentingSystem.Infrastructure.Constants;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentingSystem.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedAdmin(this IApplicationBuilder application)
        {
            using var scopedServices = application.ApplicationServices.CreateScope();
            var services = scopedServices.ServiceProvider;
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdminConstants.AdminRoleName))
                {
                    return;
                }

                var role = new IdentityRole { Name = AdminConstants.AdminRoleName };
                await roleManager.CreateAsync(role);

                var admin = await userManager.FindByNameAsync(AdminConstants.AdminEmail);

                await userManager.AddToRoleAsync(admin, role.Name);
            })
                .GetAwaiter()
                .GetResult();

            return application;
        }
    }
}
