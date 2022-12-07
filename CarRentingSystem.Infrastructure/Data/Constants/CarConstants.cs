using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentingSystem.Infrastructure.Constants
{
    public static class CarConstants
    {
        public const int TitleMaxLength = 80;
        public const int TitleMinLength = 2;
        public const int AddressMaxLength = 150;
        public const int AddressMinLength = 30;
        public const int DescriptionMaxLength = 500;
        public const int DescriptionMinLength = 50;
        public const double PricePerMonthMax = 2000.0;
        public const double PricePerMonthMin = 0.0;
    }
}
