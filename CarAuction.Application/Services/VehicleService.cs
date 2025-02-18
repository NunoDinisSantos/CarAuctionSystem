using CarAuction.Application.Repository;
using CarAuction.Application.Validations;
using CarAuction.Models.Vehicle;

namespace CarAuction.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private IVehicleRepository _vehicleRepository;
        private VehicleValidator _vehicleValidator;

        public VehicleService(IVehicleRepository vehicleRepository, VehicleValidator vehicleValidator)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleValidator = vehicleValidator;
        }

        public Task<bool> CreateVehicle(Vehicle vehicle)
        {
            var validVehicle = _vehicleValidator.CanCreateVehicle(vehicle.Id);
            validVehicle &= _vehicleValidator.ValidVehicleYear(vehicle.Year);
            validVehicle &= _vehicleValidator.ValidVehicleType(vehicle);
            validVehicle &= _vehicleValidator.ValidInitialBid(vehicle.StartingBid);

            if (!validVehicle)
            {
                return Task.FromResult(validVehicle);
            }

            _vehicleRepository.CreateVehicle(vehicle);

            return Task.FromResult(validVehicle);
        }

        public Task<bool> DeleteVehicleById(Guid id)
        {
            var result = _vehicleRepository.GetVehiclesById(id).Result;

            if(result == null)
            {
                return Task.FromResult(false);
            }

            return _vehicleRepository.DeleteVehicleById(id);
        }

        public Task<Vehicle?> GetVehiclesById(Guid id)
        {
            return _vehicleRepository.GetVehiclesById(id);
        }

        public Task<IEnumerable<Vehicle>> GetVehiclesByManufacturer(string manufacturer)
        {
            return _vehicleRepository.GetVehiclesByManufacturer(manufacturer);
        }

        public Task<IEnumerable<Vehicle>> GetVehiclesByModel(string model)
        {
            return _vehicleRepository.GetVehiclesByModel(model);
        }

        public Task<IEnumerable<Vehicle>> GetVehiclesByType(string type)
        {
            return _vehicleRepository.GetVehiclesByType(type);
        }

        public Task<IEnumerable<Vehicle>> GetVehiclesByYear(int year)
        {
            return _vehicleRepository.GetVehiclesByYear(year);
        }
    }
}
