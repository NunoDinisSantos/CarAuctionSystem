using CarAuction.Application.Repository;
using CarAuction.Models.Vehicle;

namespace CarAuction.Application.Validations
{
    public class VehicleValidator
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleValidator(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public bool CanCreateVehicle(Guid id)
        {
            var vehicle = _vehicleRepository.GetVehiclesById(id).Result;          

            return vehicle == null;
        }

        public bool ValidVehicleType(Vehicle vehicle)
        {
            switch (vehicle.Type.ToLower())
            {
                case "suv":                                     
                    return ValidateSUVFields((SUV)vehicle);
                case "hatchback":
                    return ValidateHatchbackFields((Hatchback)vehicle);
                case "sedan":
                    return ValidateSedanFields((Sedan)vehicle);
                case "truck":
                    return ValidateTruckFields((Truck)vehicle);
            }

            return false;
        }

        public bool ValidVehicleYear(int year)
        {
            return year<=DateTime.Now.Year;
        }

        public bool ValidInitialBid(double bid)
        {
            return bid >= 0;
        }

        public bool ValidateTruckFields(Truck truck)
        {
            return truck.LoadCapacity >= 0;
        }

        public bool ValidateSUVFields(SUV numberOfSeats)
        {
            return numberOfSeats.NumberOfSeats > 0;
        }

        public bool ValidateSedanFields(Sedan sedan)
        {
            return sedan.NumberOfDoors > 0;
        }

        public bool ValidateHatchbackFields(Hatchback hatchback)
        {
            return hatchback.NumberOfDoors > 0;
        }
    }
}