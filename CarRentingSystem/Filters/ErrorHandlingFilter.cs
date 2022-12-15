using CarRentingSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Serilog;

namespace CarRentingSystem.Filters
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;

            Log.Error($" {exception.Source} - {exception.Message} - {DateTime.Now}");

            context.ExceptionHandled = true; //optional 
            
            //return base.OnExceptionAsync(context);
        }
    }
}
