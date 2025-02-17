using CarAuction.Contracts.Requests;
using CarAuction.Contracts.Requests.VehicleTypes;
using CarAuction.Models.Vehicle;

namespace CarAuction.Api.Helper
{
    public class MapCar : IMapCar
    {
        public Vehicle MapVehicle(CreateSUVRequest suv)
        {
            var vehicle = new SUV()
            {
                Id = Guid.NewGuid(),
                Manufacturer = suv.Manufacturer,
                Model = suv.Model,
                StartingBid = suv.StartingBid,
                NumberOfSeats = suv.NumberOfSeats,
                Type = "suv",
                Year = suv.Year,
            };

            return vehicle;
        }

        public Vehicle MapVehicle(CreateHatchbackRequest hatchback)
        {
            var vehicle = new Hatchback()
            {
                Id = Guid.NewGuid(),
                Manufacturer = hatchback.Manufacturer,
                Model = hatchback.Model,
                StartingBid = hatchback.StartingBid,
                NumberOfDoors = hatchback.NumberOfDoors,
                Type = "hatchback",
                Year = hatchback.Year,
            };

            return vehicle;
        }

        public Vehicle MapVehicle(CreateSedanRequest sedan)
        {
            var vehicle = new Sedan()
            {
                Id = Guid.NewGuid(),
                Manufacturer = sedan.Manufacturer,
                Model = sedan.Model,
                StartingBid = sedan.StartingBid,
                NumberOfDoors = sedan.NumberOfDoors,
                Type = "sedan",
                Year = sedan.Year,
            };

            return vehicle;
        }

        public Vehicle MapVehicle(CreateTruckRequest truck)
        {
            var vehicle = new Truck()
            {
                Id = Guid.NewGuid(),
                Manufacturer = truck.Manufacturer,
                Model = truck.Model,
                StartingBid = truck.StartingBid,
                LoadCapacity = truck.LoadCapacity,
                Type = "truck",
                Year = truck.Year,
            };

            return vehicle;
        }

        public Vehicle MapVehicle(CreateVehicleRequest _vehicle)
        {
            var vehicle = new Vehicle()
            {
                Id = Guid.NewGuid(),
                Manufacturer = _vehicle.Manufacturer,
                Model = _vehicle.Model,
                StartingBid = _vehicle.StartingBid,
                Type = "vehicle",
                Year = _vehicle.Year,
            };

            return vehicle;
        }
    }
}