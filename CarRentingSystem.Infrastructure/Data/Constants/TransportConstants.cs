using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentingSystem.Infrastructure.Data.Constants
{
    public static class TransportConstants
    {
        public const int CompanyNameMaxLength = 100;
        public const int CompanyNameMinLength = 2;

        public static string CompanyNameLengthError = $"Company name must be at min length {CompanyNameMinLength} and max length {CompanyNameMaxLength}";

        public const double PricePerMonthMax = 2000.0;
        public const double PricePerMonthMin = 0.0;

        public const int DeliveryDaysMin = 1;
        public const int DeliveryDaysMax = 30;

        public const int DescriptionMaxLength = 500;
        public const int DescriptionMinLength = 10;
    }
}
