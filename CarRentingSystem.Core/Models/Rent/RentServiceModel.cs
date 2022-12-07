using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentingSystem.Core.Models.Rent
{
    public class RentServiceModel
    {
        public string CarTitle { get; set; }
        public string CarImageUrl { get; set; }
        public string AgentFullName { get; set; }
        public string AgentEmail { get; set; }
        public string AgentPhoneNumber { get; set; }
        public string RenterFullName { get; set; }
        public string RenterEmail { get; set; }
    }
}
