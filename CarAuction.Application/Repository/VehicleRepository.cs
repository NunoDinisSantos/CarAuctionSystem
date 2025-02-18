using CarAuction.Models.Vehicle;

namespace CarAuction.Application.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        //In memory database
        private readonly List<Vehicle> _vehicles = new();

        public Task<bool> CreateVehicle(Vehicle vehicle) 
        {            
            _vehicles.Add(vehicle);
           
            return Task.FromResult(true);
        }

        public Task<Vehicle?> GetVehiclesById(Guid id)
        {
            var vehicles = _vehicles.Where(x => x.Id == id).FirstOrDefault();
            return Task.FromResult(vehicles);
        }

        public Task<IEnumerable<Vehicle>> GetVehiclesByManufacturer(string manufacturer)
        {
            var vehicles = _vehicles.Where(x => x.Manufacturer.ToLower() == manufacturer.ToLower()).AsEnumerable();
            return Task.FromResult(vehicles);
        }

        public Task<IEnumerable<Vehicle>> GetVehiclesByModel(string model)
        {
            var vehicles = _vehicles.Where(x => x.Model.ToLower() == model.ToLower()).AsEnumerable();
            return Task.FromResult(vehicles);
        }

        public Task<IEnumerable<Vehicle>> GetVehiclesByType(string type)
        {
            var vehicles = _vehicles.Where(x => x.Type.ToLower() == type.ToLower()).AsEnumerable();
            return Task.FromResult(vehicles);
        }

        public Task<IEnumerable<Vehicle>> GetVehiclesByYear(int year)
        {
            var vehicles = _vehicles.Where(x => x.Year == year).AsEnumerable();
            return Task.FromResult(vehicles);
        }

        public Task<bool> DeleteVehicleById(Guid vehicleId)
        {
            var vehicleIndex = _vehicles.FindIndex(x => x.Id == vehicleId);
            _vehicles.RemoveAt(vehicleIndex);

            return Task.FromResult(!_vehicles.Any(x => x.Id == vehicleId));
        }
    }
}