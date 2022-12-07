namespace CarRentingSystem.Core.Exceptions
{
    public class CarNullException : ApplicationException
    {
        public CarNullException()
        {

        }

        public CarNullException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
