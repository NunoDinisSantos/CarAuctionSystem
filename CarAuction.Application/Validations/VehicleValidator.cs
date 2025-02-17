using CarAuction.Models.Vehicle;

namespace CarAuction.Application.Validations
{
    public class VehicleValidator
    {
        public bool CanCreateVehicle(Guid id, List<Vehicle> fakeDb)
        {
            return !fakeDb.Any(x => x.Id == id);
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