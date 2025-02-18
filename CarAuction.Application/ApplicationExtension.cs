using CarAuction.Application.Repository;
using CarAuction.Application.Services;
using CarAuction.Application.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace CarAuction.Application
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IAuctionRepository, AuctionRepository>();
            services.AddSingleton<IVehicleRepository, VehicleRepository>();
            services.AddSingleton<IAuctionService, AuctionService>();
            services.AddSingleton<IVehicleService, VehicleService>();
            services.AddSingleton<VehicleValidator>();
            services.AddSingleton<AuctionValidator>();
            return services;
        }
    }
}
