using CarRentingSystem.Core.Contracts;
using System.Text;
using System.Text.RegularExpressions;

namespace CarRentingSystem.Core.Extensions
{
    public static class ModelExtensions
    {
        public static string GetInformation(this ICarModel car)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(car.Title.Replace(" ", "-"));
            sb.Append("-");
            sb.Append(GetAddress(car.Address));

            return sb.ToString().TrimEnd();
        }

        private static string GetAddress(string address)
        {
            string result = String.Join("-", address.Split(" ", StringSplitOptions.RemoveEmptyEntries).Take(3));
            return Regex.Replace(address, @"[^a-zA-Z0-9\-]", string.Empty);
        }
    }
}
