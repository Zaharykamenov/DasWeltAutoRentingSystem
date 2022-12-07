using CarRentingSystem.Infrastructure.Constants;
using System.Security.Claims;

namespace CarRentingSystem.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdminConstants.AdminRoleName);
        }
    }
}
