using CarAuction.Api.Helper;
using CarAuction.Contracts.Requests;
using CarAuction.Contracts.Requests.VehicleTypes;

namespace CarAuction.Tests.Mapping
{
    public class MapCarUnitTest
    {
        private readonly MapCar _mapCar;

        public MapCarUnitTest()
        {
            _mapCar = new MapCar();
        }

        [Fact]
        public void MapVehicle_ShouldMapToSUV_WhenReceivesSUVRequest()
        {
            var suvRequest = new CreateSUVRequest()
            {
                Model = "Model",
                Manufacturer = "Manufacturer",
                StartingBid = 35000,
                NumberOfSeats = 5,
                Year = 2020
            };

            var result = _mapCar.MapVehicle(suvRequest);

            Assert.Equal("SUV", result.GetType().Name);
        }

        [Fact]
        public void MapVehicle_ShouldMapToTruck_WhenReceivesTruckRequest()
        {
            var truckRequest = new CreateTruckRequest()
            {
                Model = "Model",
                Manufacturer = "Manufacturer",
                StartingBid = 35000,
                LoadCapacity = 6554.5,
                Year = 2020
            };

            var result = _mapCar.MapVehicle(truckRequest);

            Assert.Equal("Truck", result.GetType().Name);
        }

        [Fact]
        public void MapVehicle_ShouldMapToSedan_WhenReceivesSedanRequest()
        {
            var sedanRequest = new CreateSedanRequest()
            {
                Model = "Model",
                Manufacturer = "Manufacturer",
                StartingBid = 35000,
                NumberOfDoors = 5,
                Year = 2020
            };

            var result = _mapCar.MapVehicle(sedanRequest);

            Assert.Equal("Sedan", result.GetType().Name);
        }

        [Fact]
        public void MapVehicle_ShouldMapToHatchback_WhenReceivesHatchbackRequest()
        {
            var hatchBackRequest = new CreateHatchbackRequest()
            {
                Model = "Model",
                Manufacturer = "Manufacturer",
                StartingBid = 35000,
                NumberOfDoors = 3,
                Year = 2020,      
            };

            var result = _mapCar.MapVehicle(hatchBackRequest);

            Assert.Equal("Hatchback", result.GetType().Name);
        }

        [Fact]
        public void MapVehicle_ShouldMapToDefaultVehicle_WhenReceivesInvalidType()
        {
            var defaultVehicleRequest = new CreateVehicleRequest()
            {
                Model = "Model",
                Manufacturer = "Manufacturer",
                StartingBid = 35000,
                Year = 2020,
            };

            var result = _mapCar.MapVehicle(defaultVehicleRequest);

            Assert.Equal("Vehicle", result.GetType().Name);
        }
    }
}