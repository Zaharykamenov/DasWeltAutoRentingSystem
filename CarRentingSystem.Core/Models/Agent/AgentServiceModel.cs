using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentingSystem.Core.Models.Agent
{
    public class AgentServiceModel
    {
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; } = null;
        public string FullName { get; set; } = null!;
    }
}
