using CarAuction.Application.Repository;
using CarAuction.Application.Services;
using CarAuction.Application.Validations;
using CarAuction.Models.Vehicle;
using NSubstitute;

namespace CarAuction.Tests.Service
{
    public class VehicleServiceTests
    {
        private readonly VehicleService _vehicleService;
        private readonly VehicleValidator _vehicleValidator;
        private readonly IVehicleRepository _vehicleRepository = Substitute.For<IVehicleRepository>();

        public VehicleServiceTests()
        {
            _vehicleValidator = new VehicleValidator(_vehicleRepository);
            _vehicleService = new VehicleService(_vehicleRepository, _vehicleValidator);
        }

        [Fact]
        public void CreateVehicle_ShouldCreateSUVVehicle_WhenValidationsAreCorrect()
        {
            var vehicle = new SUV()
            {
                Id = Guid.NewGuid(),
                StartingBid = 32000,
                Manufacturer = "TestManufacturer",
                Model = "TestModel",
                Type = "SUV",
                Year = 2020,
                NumberOfSeats = 5
            };

            var result = _vehicleService.CreateVehicle(vehicle).Result;

            Assert.True(result);
        }

        [Fact]
        public void CreateVehicle_ShouldCreateHatchbackVehicle_WhenValidationsAreCorrect()
        {
            var vehicle = new Hatchback()
            {
                Id = Guid.NewGuid(),
                StartingBid = 32000,
                Manufacturer = "TestManufacturer",
                Model = "TestModel",
                Type = "Hatchback",
                Year = 2020,
                NumberOfDoors = 3
            };

            var result = _vehicleService.CreateVehicle(vehicle).Result;

            Assert.True(result);
        }

        [Fact]
        public void CreateVehicle_ShouldCreateTruckVehicle_WhenValidationsAreCorrect()
        {
            var vehicle = new Truck()
            {
                Id = Guid.NewGuid(),
                StartingBid = 32000,
                Manufacturer = "TestManufacturer",
                Model = "TestModel",
                Type = "Truck",
                Year = 2020,
                LoadCapacity = 555.5
            };

            var result = _vehicleService.CreateVehicle(vehicle).Result;

            Assert.True(result);
        }

        [Fact]
        public void CreateVehicle_ShouldCreateSedanVehicle_WhenValidationsAreCorrect()
        {
            var vehicle = new Sedan()
            {
                Id = Guid.NewGuid(),
                StartingBid = 32000,
                Manufacturer = "TestManufacturer",
                Model = "TestModel",
                Type = "Sedan",
                Year = 2020,
                NumberOfDoors = 5
            };

            var result = _vehicleService.CreateVehicle(vehicle).Result;

            Assert.True(result);
        }

        [Fact]
        public void CreateVehicle_ShouldNotCreateVehicle_WhenIdAlreadyExists()
        {
            var guid = Guid.NewGuid();
            var vehicle = new SUV()
            {
                Id = guid,
                StartingBid = 32000,
                Manufacturer = "TestManufacturer",
                Model = "TestModel",
                Type = "SUV",
                Year = 2020,
                NumberOfSeats = 5
            };

            _vehicleRepository.GetVehiclesById(Arg.Any<Guid>()).Returns(vehicle);

            var result = _vehicleService.CreateVehicle(vehicle).Result;

            Assert.False(result);
        }

        [Fact]
        public void CreateVehicle_ShouldNotCreateVehicle_WhenYearIsInvalid()
        {
            var vehicle = new SUV()
            {
                Id = Guid.NewGuid(),
                StartingBid = 32000,
                Manufacturer = "TestManufacturer",
                Model = "TestModel",
                Type = "SUV",
                Year = DateTime.Now.Year+1,
                NumberOfSeats = 5
            };

            var result = _vehicleService.CreateVehicle(vehicle).Result;

            Assert.False(result);
        }

        [Fact]
        public void CreateVehicle_ShouldNotCreateVehicle_WhenTypeIsInvalid()
        {
            var vehicle = new SUV()
            {
                Id = Guid.NewGuid(),
                StartingBid = 32000,
                Manufacturer = "TestManufacturer",
                Model = "TestModel",
                Type = "InvalidType",
                Year = 2020,
                NumberOfSeats = 5
            };

            var result = _vehicleService.CreateVehicle(vehicle).Result;

            Assert.False(result);
        }

        [Fact]
        public void CreateVehicle_ShouldNotCreateVehicle_WhenInitialBidIsInvalid()
        {
            var vehicle = new SUV()
            {
                Id = Guid.NewGuid(),
                StartingBid = -32000,
                Manufacturer = "TestManufacturer",
                Model = "TestModel",
                Type = "SUV",
                Year = 2020,
                NumberOfSeats = 5
            };

            var result = _vehicleService.CreateVehicle(vehicle).Result;

            Assert.False(result);
        }

        [Fact]
        public void GetVehiclesByType_ShouldReturnVehiclesByType_WhenVehiclesExist()
        {
            var expectedVehicleList = new List<Vehicle>()
            { 
                new SUV()
                {
                    Id = Guid.NewGuid(),
                    StartingBid = 12000,
                    NumberOfSeats = 5,
                    Manufacturer = "Manufacturer",
                    Model = "Model",
                    Type = "Type1",
                    Year = 2020
                },
                new SUV()
                {
                    Id = Guid.NewGuid(),
                    StartingBid = 17000,
                    NumberOfSeats = 5,
                    Manufacturer = "Manufacturer",
                    Model = "Model",
                    Type = "Type1",
                    Year = 2020
                },
            };

            _vehicleRepository.GetVehiclesByType("Type1").Returns(expectedVehicleList);

            var result = _vehicleService.GetVehiclesByType("Type1").Result;

            Assert.Equal(2,result.Count());
        }

        [Fact]
        public void GetVehiclesByType_ShouldReturnEmptyEnumerator_WhenVehiclesWithTypeDoNotExist()
        {
            var expectedVehicleList = new List<Vehicle>()
            {
                new SUV()
                {
                    Id = Guid.NewGuid(),
                    StartingBid = 12000,
                    NumberOfSeats = 5,
                    Manufacturer = "Manufacturer",
                    Model = "Model",
                    Type = "Type1",
                    Year = 2020
                },
                new SUV()
                {
                    Id = Guid.NewGuid(),
                    StartingBid = 17000,
                    NumberOfSeats = 5,
                    Manufacturer = "Manufacturer",
                    Model = "Model",
                    Type = "Type1",
                    Year = 2020
                },
            };

            _vehicleRepository.GetVehiclesByType("Type1").Returns(expectedVehicleList);

            var result = _vehicleService.GetVehiclesByType("Type2").Result;

            Assert.Empty(result);
        }

        [Fact]
        public void GetVehiclesByYear_ShouldReturnVehiclesByYear_WhenVehiclesExist()
        {
            var expectedVehicleList = new List<Vehicle>()
            {
                new SUV()
                {
                    Id = Guid.NewGuid(),
                    StartingBid = 12000,
                    NumberOfSeats = 5,
                    Manufacturer = "Manufacturer",
                    Model = "Model",
                    Type = "Type",
                    Year = 2025
                },
                new SUV()
                {
                    Id = Guid.NewGuid(),
                    StartingBid = 17000,
                    NumberOfSeats = 5,
                    Manufacturer = "Manufacturer",
                    Model = "Model",
                    Type = "Type",
                    Year = 2025
                },
            };

            _vehicleRepository.GetVehiclesByYear(2025).Returns(expectedVehicleList);

            var result = _vehicleService.GetVehiclesByYear(2025).Result;

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetVehiclesByYear_ShouldReturnEmptyEnumerator_WhenVehiclesWithYearDoNotExist()
        {
            var expectedVehicleList = new List<Vehicle>()
            {
                new SUV()
                {
                    Id = Guid.NewGuid(),
                    StartingBid = 12000,
                    NumberOfSeats = 5,
                    Manufacturer = "Manufacturer",
                    Model = "Model",
                    Type = "Type",
                    Year = 2025
                },
                new SUV()
                {
                    Id = Guid.NewGuid(),
                    StartingBid = 17000,
                    NumberOfSeats = 5,
                    Manufacturer = "Manufacturer",
                    Model = "Model",
                    Type = "Type",
                    Year = 2025
                },
            };

            _vehicleRepository.GetVehiclesByYear(2025).Returns(expectedVehicleList);

            var result = _vehicleService.GetVehiclesByYear(2020).Result;

            Assert.Empty(result);
        }

        [Fact]
        public void GetVehiclesByModel_ShouldReturnVehiclesByModel_WhenVehiclesExist()
        {
            var expectedVehicleList = new List<Vehicle>()
            {
                new SUV()
                {
                    Id = Guid.NewGuid(),
                    StartingBid = 12000,
                    NumberOfSeats = 5,
                    Manufacturer = "Manufacturer",
                    Model = "Model1",
                    Type = "Type",
                    Year = 2025
                },
                new SUV()
                {
                    Id = Guid.NewGuid(),
                    StartingBid = 17000,
                    NumberOfSeats = 5,
                    Manufacturer = "Manufacturer",
                    Model = "Model1",
                    Type = "Type",
                    Year = 2025
                },
            };

            _vehicleRepository.GetVehiclesByModel("Model1").Returns(expectedVehicleList);

            var result = _vehicleService.GetVehiclesByModel("Model1").Result;

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetVehiclesByModel_ShouldReturnEmptyEnumerator_WhenVehiclesWithModelDoNotExist()
        {
            var expectedVehicleList = new List<Vehicle>()
            {
                new SUV()
                {
                    Id = Guid.NewGuid(),
                    StartingBid = 12000,
                    NumberOfSeats = 5,
                    Manufacturer = "Manufacturer",
                    Model = "Model",
                    Type = "Type",
                    Year = 2025
                },
                new SUV()
                {
                    Id = Guid.NewGuid(),
                    StartingBid = 17000,
                    NumberOfSeats = 5,
                    Manufacturer = "Manufacturer",
                    Model = "Model",
                    Type = "Type", 
                    Year = 2025
                },
            };

            _vehicleRepository.GetVehiclesByModel("Model").Returns(expectedVehicleList);

            var result = _vehicleService.GetVehiclesByModel("Model1").Result;

            Assert.Empty(result);
        }

        [Fact]
        public void GetVehiclesByManufacturer_ShouldReturnVehiclesByManufacturer_WhenVehiclesExist()
        {
            var expectedVehicleList = new List<Vehicle>()
            {
                new SUV()
                {
                    Id = Guid.NewGuid(),
                    StartingBid = 12000,
                    NumberOfSeats = 5,
                    Manufacturer = "Manufacturer",
                    Model = "Model",
                    Type = "Type",
                    Year = 2025
                },
                new SUV()
                {
                    Id = Guid.NewGuid(),
                    StartingBid = 17000,
                    NumberOfSeats = 5,
                    Manufacturer = "Manufacturer",
                    Model = "Model",
                    Type = "Type",
                    Year = 2025
                },
            };

            _vehicleRepository.GetVehiclesByManufacturer("Manufacturer").Returns(expectedVehicleList);

            var result = _vehicleService.GetVehiclesByManufacturer("Manufacturer").Result;

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetVehiclesByManufacturer_ShouldReturnEmptyEnumerator_WhenVehiclesWithManufacturerDoNotExist()
        {
            var expectedVehicleList = new List<Vehicle>()
            {
                new SUV()
                {
                    Id = Guid.NewGuid(),
                    StartingBid = 12000,
                    NumberOfSeats = 5,
                    Manufacturer = "Manufacturer",
                    Model = "Model",
                    Type = "Type",
                    Year = 2025
                },
                new SUV()
                {
                    Id = Guid.NewGuid(),
                    StartingBid = 17000,
                    NumberOfSeats = 5,
                    Manufacturer = "Manufacturer",
                    Model = "Model",
                    Type = "Type",
                    Year = 2025
                },
            };

            _vehicleRepository.GetVehiclesByManufacturer("Manufacturer").Returns(expectedVehicleList);

            var result = _vehicleService.GetVehiclesByModel("Manufacturer1").Result;

            Assert.Empty(result);
        }
    }
}
