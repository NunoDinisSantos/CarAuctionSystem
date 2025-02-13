using CarAuction.Models.Vehicle;

namespace CarAuction.Application.Repository
{
    internal interface IVehicleRepository
    {
        Task<bool> CreateAsync(Vehicle vehicle);

        Task<IEnumerable<Vehicle>> GetVehiclesByType(string type);

        Task<IEnumerable<Vehicle>> GetVehiclesByManufacturer(string manufacturer);

        Task<IEnumerable<Vehicle>> GetVehiclesByModel(string manufacturer);

        Task<IEnumerable<Vehicle>> GetVehiclesByYear(int year);
    }
}
