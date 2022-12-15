using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentingSystem.Infrastructure.Data.Models
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
        public string Source { get; set; } = null!;
        public DateTime RegistedOn { get; set; }
        public string User { get; set; } = null!;
    }
}
