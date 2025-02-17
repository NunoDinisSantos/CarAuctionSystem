using CarAuction.Application.Validations;
using CarAuction.Models.Vehicle;

namespace CarAuction.Application.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        //In memory database
        private readonly List<Vehicle> _vehicles = new();

        private VehicleValidator _vehicleValidator = new();

        public Task<bool> CreateVehicle(Vehicle vehicle) 
        {
            var validVehicle = _vehicleValidator.CanCreateVehicle(vehicle.Id, _vehicles);
            validVehicle &= _vehicleValidator.ValidVehicleYear(vehicle.Year);
            validVehicle &= _vehicleValidator.ValidVehicleType(vehicle);
            validVehicle &= _vehicleValidator.ValidInitialBid(vehicle.StartingBid);

            if(validVehicle)
            {
                _vehicles.Add(vehicle);
            }

            return Task.FromResult(validVehicle);
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
    }
}