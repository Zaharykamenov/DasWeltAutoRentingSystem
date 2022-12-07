using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentingSystem.Core.Contracts
{
    public interface ICarModel
    {
        public string Title { get; }
        public string Address { get; }
    }
}
