using CarRentingSystem.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace CarRentingSystem.Core.Models.Agent
{
    public class BecomeAgentModel
    {
        [Required]
        [StringLength(AgentConstants.BecomeAgentModePhoneNumberMaxLength, MinimumLength = AgentConstants.BecomeAgentModePhoneNumberMinLength)]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = null!;
    }
}
