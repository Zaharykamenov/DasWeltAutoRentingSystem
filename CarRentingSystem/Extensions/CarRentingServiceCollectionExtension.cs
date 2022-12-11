using CarRentingSystem.Common;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Exceptions;
using CarRentingSystem.Core.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CarRentingServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IAgentService, AgentService>();
            services.AddScoped<IGuard, Guard>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRentService, RentService>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddScoped<IEngineCategoryService, EngineCategoryService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITransportService, TransportService>();

            services.AddMemoryCache();

            return services;
        }
    }
}
