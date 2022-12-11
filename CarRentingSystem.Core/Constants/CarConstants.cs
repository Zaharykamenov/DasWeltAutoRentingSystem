

namespace CarRentingSystem.Core.Constants
{
    public static class CarConstants
    {
        public const int TitleMaxLength = 50;
        public const int TitleMinLength = 10;
        public const int AddressMaxLength = 500;
        public const int AddressMinLength = 10;
        public const int DescriptionMaxLength = 500;
        public const int DescriptionMinLength = 50;
        public const double PricePerMonthMax = 999999999.99;
        public const double PricePerMonthMin = 0.0;

        public const string ParametersAreNullOrEmptyError = "Some of parameters are null or empty!";
    }
}
