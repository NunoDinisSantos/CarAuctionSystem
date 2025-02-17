using CarAuction.Application.Validations;
using CarAuction.Models.Vehicle;

namespace CarAuction.Tests.Validations
{
    public class VehicleValidatorUnitTest
    {
        private readonly VehicleValidator _vehicleValidator;

        public VehicleValidatorUnitTest()
        {
            _vehicleValidator = new VehicleValidator();
        }

        [Fact]
        public void CanCreateVehicle_ShouldCreateVehicle_WhenIdIsUnique()
        {
            var vehicle = new Vehicle()
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Manufacturer",
                StartingBid = 25000,
                Model = "Model",
                Type = "Type",
                Year = 2020
            };

            var fakeDb = new List<Vehicle>();

            var result = _vehicleValidator.CanCreateVehicle(vehicle.Id,fakeDb);

            Assert.True(result);
        }

        [Fact]
        public void CanCreateVehicle_ShouldNotCreateVehicle_WhenIdIsNotUnique()
        {
            var usedGuid = Guid.NewGuid();

            var vehicle = new Vehicle()
            {
                Id = usedGuid,
                Manufacturer = "Manufacturer",
                StartingBid = 25000,
                Model = "Model",
                Type = "Type",
                Year = 2020
            };

            var fakeDb = new List<Vehicle>()
            {
                new Vehicle()
                {
                    Id = usedGuid,
                    StartingBid = 20000,
                    Model = "Model",
                    Type = "Type",
                    Year = 2021,
                    Manufacturer = "Manufacturer"
                }
            };
                
            var result = _vehicleValidator.CanCreateVehicle(vehicle.Id, fakeDb);

            Assert.False(result);
        }

        [Fact]
        public void ValidVehicleType_ShouldNotCreateVehicle_WhenTypeIsInvalid()
        {
            var vehicle = new Vehicle()
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Manufacturer",
                StartingBid = 25000,
                Model = "Model",
                Type = "Type",
                Year = 2020
            };

            var result = _vehicleValidator.ValidVehicleType(vehicle);

            Assert.False(result);
        }

        [Fact]
        public void ValidVehicleType_ShouldCreateTruckVehicle_WhenTypeIsValid()
        {
            var vehicle = new Truck()
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Manufacturer",
                StartingBid = 25000,
                Model = "Model",
                Type = "truck",
                Year = 2020,
                LoadCapacity = 1000
            };

            var result = _vehicleValidator.ValidVehicleType(vehicle);

            Assert.True(result);
        }

        [Fact]
        public void ValidVehicleType_ShouldCreateHatchbackVehicle_WhenTypeIsValid()
        {
            var vehicle = new Hatchback()
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Manufacturer",
                StartingBid = 25000,
                Model = "Model",
                Type = "hatchback",
                Year = 2020,
                NumberOfDoors = 5
            };

            var result = _vehicleValidator.ValidVehicleType(vehicle);

            Assert.True(result);
        }

        [Fact]
        public void ValidVehicleType_ShouldCreateSUVVehicle_WhenTypeIsValid()
        {
            var vehicle = new SUV()
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Manufacturer",
                StartingBid = 25000,
                Model = "Model",
                Type = "suv",
                Year = 2020,
                NumberOfSeats = 5
            };

            var result = _vehicleValidator.ValidVehicleType(vehicle);

            Assert.True(result);
        }

        [Fact]
        public void ValidVehicleType_ShouldCreateSedanVehicle_WhenTypeIsValid()
        {
            var vehicle = new Sedan()
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Manufacturer",
                StartingBid = 25000,
                Model = "Model",
                Type = "sedan",
                Year = 2020,
                NumberOfDoors = 3
            };

            var result = _vehicleValidator.ValidVehicleType(vehicle);

            Assert.True(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(30000)]
        public void ValidInitialBid_ShouldCreateVehicle_WhenBidIsValid(int bid)
        {
            var vehicle = new Vehicle()
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Manufacturer",
                StartingBid = bid,
                Model = "Model",
                Type = "Type",
                Year = 2020
            };

            var result = _vehicleValidator.ValidInitialBid(vehicle.StartingBid);

            Assert.True(result);
        }

        [Fact]
        public void ValidInitialBid_ShouldNotCreateVehicle_WhenBidIsInvalid()
        {
            var vehicle = new Vehicle()
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Manufacturer",
                StartingBid = -1,
                Model = "Model",
                Type = "Type",
                Year = 2020
            };

            var result = _vehicleValidator.ValidInitialBid(vehicle.StartingBid);

            Assert.False(result);
        }

        [Theory]
        [InlineData(2020)]
        [InlineData(2000)]
        [InlineData(2015)]
        public void ValidVehicleYear_ShouldCreateVehicle_WhenDateIsValid(int year)
        {
            var vehicle = new Vehicle()
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Manufacturer",
                StartingBid = 15000,
                Model = "Model",
                Type = "Type",
                Year = year
            };

            var result = _vehicleValidator.ValidVehicleYear(vehicle.Year);

            Assert.True(result);
        }

        [Fact]
        public void ValidVehicleYear_ShouldNotCreateVehicle_WhenDateIsInFuture()
        {
            var year = DateTime.Now.Year + 1;
            var vehicle = new Vehicle()
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Manufacturer",
                StartingBid = 15000,
                Model = "Model",
                Type = "Type",
                Year = year
            };

            var result = _vehicleValidator.ValidVehicleYear(vehicle.Year);

            Assert.False(result);
        }

        [Fact]
        public void ValidateSUVFields_ShouldCreateVehicle_WhenDataIsValid()
        {
            var vehicle = new SUV()
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Manufacturer",
                StartingBid = 15000,
                Model = "Model",
                Type = "Type",
                Year = DateTime.Now.Year,
                NumberOfSeats = 5
            };

            var result = _vehicleValidator.ValidateSUVFields(vehicle);

            Assert.True(result);
        }

        [Fact]
        public void ValidateSUVFields_ShouldNotCreateVehicle_WhenDataIsInvalid()
        {
            var vehicle = new SUV()
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Manufacturer",
                StartingBid = 15000,
                Model = "Model",
                Type = "Type",
                Year = DateTime.Now.Year,
                NumberOfSeats = -1
            };

            var result = _vehicleValidator.ValidateSUVFields(vehicle);

            Assert.False(result);
        }

        [Fact]
        public void ValidateTruckFields_ShouldCreateVehicle_WhenDataIsValid()
        {
            var vehicle = new Truck()
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Manufacturer",
                StartingBid = 15000,
                Model = "Model",
                Type = "Type",
                Year = DateTime.Now.Year,
                LoadCapacity = 1500.5
            };

            var result = _vehicleValidator.ValidateTruckFields(vehicle);

            Assert.True(result);
        }

        [Fact]
        public void ValidateTruckFields_ShouldNotCreateVehicle_WhenDataIsInvalid()
        {
            var vehicle = new Truck()
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Manufacturer",
                StartingBid = 15000,
                Model = "Model",
                Type = "Type",
                Year = DateTime.Now.Year,
                LoadCapacity = -1.5
            };

            var result = _vehicleValidator.ValidateTruckFields(vehicle);

            Assert.False(result);
        }

        [Fact]
        public void ValidateSedanFields_ShouldCreateVehicle_WhenDataIsValid()
        {
            var vehicle = new Sedan()
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Manufacturer",
                StartingBid = 15000,
                Model = "Model",
                Type = "Type",
                Year = DateTime.Now.Year,
                NumberOfDoors = 3
            };

            var result = _vehicleValidator.ValidateSedanFields(vehicle);

            Assert.True(result);
        }

        [Fact]
        public void ValidateSedanFields_ShouldNotCreateVehicle_WhenDataIsInvalid()
        {
            var vehicle = new Sedan()
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Manufacturer",
                StartingBid = 15000,
                Model = "Model",
                Type = "Type",
                Year = DateTime.Now.Year,
                NumberOfDoors = -3
            };

            var result = _vehicleValidator.ValidateSedanFields(vehicle);

            Assert.False(result);
        }

        [Fact]
        public void ValidateHatchbackFields_ShouldCreateVehicle_WhenDataIsValid()
        {
            var vehicle = new Hatchback()
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Manufacturer",
                StartingBid = 15000,
                Model = "Model",
                Type = "Type",
                Year = DateTime.Now.Year,
                NumberOfDoors = 3
            };

            var result = _vehicleValidator.ValidateHatchbackFields(vehicle);

            Assert.True(result);
        }

        [Fact]
        public void ValidateHatchbackFields_ShouldNotCreateVehicle_WhenDataIsInvalid()
        {
            var vehicle = new Hatchback()
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Manufacturer",
                StartingBid = 15000,
                Model = "Model",
                Type = "Type",
                Year = DateTime.Now.Year,
                NumberOfDoors = -3
            };

            var result = _vehicleValidator.ValidateHatchbackFields(vehicle);

            Assert.False(result);
        }
    }
}