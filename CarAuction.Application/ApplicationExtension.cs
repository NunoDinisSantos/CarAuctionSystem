using CarAuction.Application.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CarAuction.Application
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IAuctionRepository, AuctionRepository>();
            services.AddSingleton<IVehicleRepository, VehicleRepository>();
            return services;
        }
    }
}
