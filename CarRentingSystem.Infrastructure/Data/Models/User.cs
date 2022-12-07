using CarRentingSystem.Infrastructure.Data.Constants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarRentingSystem.Infrastructure.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(UserConstants.UserFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(UserConstants.UserLastNameMaxLength)]
        public string LastName { get; set; } = null!;


    }
}
