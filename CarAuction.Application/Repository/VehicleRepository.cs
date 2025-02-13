using CarAuction.Models.Vehicle;

namespace CarAuction.Application.Repository
{
    internal class VehicleRepository : IVehicleRepository
    {
        public Task<bool> CreateAsync(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vehicle>> GetVehiclesByManufacturer(string manufacturer)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vehicle>> GetVehiclesByModel(string manufacturer)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vehicle>> GetVehiclesByType(string type)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vehicle>> GetVehiclesByYear(int year)
        {
            throw new NotImplementedException();
        }
    }
}
