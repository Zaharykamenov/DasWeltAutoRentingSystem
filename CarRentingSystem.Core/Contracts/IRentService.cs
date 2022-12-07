using CarRentingSystem.Core.Models.Rent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentingSystem.Core.Contracts
{
    public interface IRentService
    {
        Task<IEnumerable<RentServiceModel>> All();
    }
}
