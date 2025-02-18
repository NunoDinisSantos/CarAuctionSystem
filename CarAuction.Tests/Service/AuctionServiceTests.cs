using CarAuction.Application.Repository;
using CarAuction.Application.Services;
using CarAuction.Application.Validations;
using CarAuction.Models.Auction;
using CarAuction.Models.Vehicle;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.ReturnsExtensions;

namespace CarAuction.Tests.Service
{
    public class AuctionServiceTests
    {
        private readonly AuctionService _auctionService;
        private readonly AuctionValidator _auctionValidator;
        private readonly IAuctionRepository _auctionRepository = Substitute.For<IAuctionRepository>();

        public AuctionServiceTests()
        {
            _auctionValidator = new AuctionValidator(_auctionRepository);
            _auctionService = new AuctionService(_auctionRepository, _auctionValidator);
        }

        [Fact]
        public void CreateAuction_ShouldCreateAuction_WhenDataIsCorrect()
        {
            var endDate = DateTime.Now.AddDays(7);
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

            _auctionRepository.ExistsById(Arg.Any<Guid>()).Returns(false);
            _auctionRepository.UniqueCarForAuction(Arg.Any<Guid>()).Returns(true);
            _auctionRepository.CreateAuction(Arg.Any<Auction>()).Returns(true);

            var result = _auctionService.CreateAuction(vehicle,endDate).Result;

            Assert.True(result);
        }

        [Fact]
        public void CreateAuction_ShouldNotCreateAuction_WhenAuctionExists()
        {
            var endDate = DateTime.Now.AddDays(7);
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

            _auctionRepository.ExistsById(Arg.Any<Guid>()).Returns(true);
            _auctionRepository.UniqueCarForAuction(Arg.Any<Guid>()).Returns(true);

            var result = _auctionService.CreateAuction(vehicle, endDate).Result;

            Assert.False(result);
        }

        [Fact]
        public void CreateAuction_ShouldNotCreateAuction_WhenDateIsInvalid()
        {
            var endDate = DateTime.Now.AddDays(-7);
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

            _auctionRepository.UniqueCarForAuction(Arg.Any<Guid>()).Returns(false);
            _auctionRepository.ExistsById(Arg.Any<Guid>()).Returns(false);

            var result = _auctionService.CreateAuction(vehicle, endDate).Result;

            Assert.False(result);
        }

        [Fact]
        public void CreateAuction_ShouldNotCreateAuction_WhenVehicleAlreadyIsInAuction()
        {
            var endDate = DateTime.Now.AddDays(7);
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


            _auctionRepository.UniqueCarForAuction(Arg.Any<Guid>()).Returns(false);
            _auctionRepository.ExistsById(Arg.Any<Guid>()).Returns(false);

            var result = _auctionService.CreateAuction(vehicle, endDate).Result;

            Assert.False(result);
        }

        [Fact]
        public void GetAllAuctions_ShouldReturnAuctions_WhenAuctionExists()
        {
            var auctions = new List<Auction>()
            {
                new Auction()
                {
                    Id = Guid.NewGuid(),
                    CarId = Guid.NewGuid(),
                    CurrentBid = 45000,
                    EndDate = DateTime.Now.AddDays(2),
                    isActive = true,
                },
                new Auction()
                {
                    Id = Guid.NewGuid(),
                    CarId = Guid.NewGuid(),
                    CurrentBid = 35000,
                    EndDate = DateTime.Now.AddDays(1),
                    isActive = true,
                }
            };

            _auctionRepository.GetAllAuctions().Returns(auctions);

            var result = _auctionService.GetAllAuctions().Result;

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetAuctionById_ShouldReturnEmpty_WhenAuctionDoesNotExist()
        {
            var result = _auctionService.GetAuctionById(Guid.NewGuid()).Result;

            Assert.Null(result);
        }

        [Fact]
        public void GetAuctionById_ShouldReturnCorrectAuction_WhenAuctionExists()
        {
            var auction = new Auction()
            {
                Id = Guid.NewGuid(),
                CarId = Guid.NewGuid(),
                CurrentBid = 45000,
                EndDate = DateTime.Now.AddDays(2),
                isActive = true,            
            };

            _auctionRepository.GetAuctionById(Arg.Any<Guid>()).Returns(auction);

            var result = _auctionService.GetAuctionById(Guid.NewGuid()).Result;

            Assert.Equal(result,auction);
        }

        [Fact]
        public void UpdateAuctionActiveState_ShouldReturnNull_WhenAuctionDoesNotExist()
        {
            _auctionRepository.GetAuctionById(Arg.Any<Guid>()).ReturnsNull();

            var result = _auctionService.UpdateAuctionActiveState(Guid.NewGuid()).Result;

            Assert.Null(result);
        }

        [Fact]
        public void UpdateAuctionActiveState_ShouldCloseAuction_WhenAuctionExists()
        {
            var auction = new Auction()
            {
                Id = Guid.NewGuid(),
                CarId = Guid.NewGuid(),
                CurrentBid = 45000,
                EndDate = DateTime.Now.AddDays(2),
                isActive = true,
            };

            _auctionRepository.GetAuctionById(Arg.Any<Guid>()).Returns(auction);
            _auctionRepository.UpdateAuctionActiveState(Arg.Any<Auction>()).Returns(true);

            var result = _auctionService.UpdateAuctionActiveState(Guid.NewGuid()).Result;

            Assert.Equal(auction.Id, result.Id);
            Assert.False(result.isActive);
        }

        [Fact]
        public void UpdateAuctionBid_ShouldReturnNull_WhenAuctionDoesNotExist()
        {
            var newBid = 35000;

            _auctionRepository.GetAuctionById(Arg.Any<Guid>()).ReturnsNull();
            var result = _auctionService.UpdateAuctionBid(Guid.NewGuid(),newBid).Result;

            Assert.False(result);
        }

        [Fact]
        public void UpdateAuctionBid_ShouldNotUpdateBid_WhenAuctionExistsWithHigherBid()
        {
            var newBid = 35000;
            var auction = new Auction()
            {
                Id = Guid.NewGuid(),
                CarId = Guid.NewGuid(),
                CurrentBid = 45000,
                EndDate = DateTime.Now.AddDays(2),
                isActive = true,
            };

            _auctionRepository.GetAuctionById(Arg.Any<Guid>()).Returns(auction);

            var result = _auctionService.UpdateAuctionBid(Guid.NewGuid(), newBid).Result;

            Assert.False(result);
        }

        [Fact]
        public void UpdateAuctionBid_ShouldNotUpdateBid_WhenAuctionIsClosed()
        {
            var newBid = 65000;
            var auction = new Auction()
            {
                Id = Guid.NewGuid(),
                CarId = Guid.NewGuid(),
                CurrentBid = 45000,
                EndDate = DateTime.Now.AddDays(2),
                isActive = false,
            };

            _auctionRepository.GetAuctionById(Arg.Any<Guid>()).Returns(auction);

            var result = _auctionService.UpdateAuctionBid(Guid.NewGuid(), newBid).Result;

            Assert.False(result);
        }

        [Fact]
        public void UpdateAuctionBid_ShouldUpdateBid_WhenAuctionExistsWithLowerBid()
        {
            var newBid = 65000;
            var auction = new Auction()
            {
                Id = Guid.NewGuid(),
                CarId = Guid.NewGuid(),
                CurrentBid = 45000,
                EndDate = DateTime.Now.AddDays(2),
                isActive = true,
            };

            _auctionRepository.GetAuctionById(Arg.Any<Guid>()).Returns(auction);

            var result = _auctionService.UpdateAuctionBid(Guid.NewGuid(), newBid).Result;

            Assert.True(result);
        }


        [Fact]
        public void UpdateAuctionBid_ShouldNotUpdateBid_WhenAuctionDateEnded()
        {
            var newBid = 65000;
            var auction = new Auction()
            {
                Id = Guid.NewGuid(),
                CarId = Guid.NewGuid(),
                CurrentBid = 45000,
                EndDate = DateTime.Now.AddDays(-1),
                isActive = true,
            };

            _auctionRepository.GetAuctionById(Arg.Any<Guid>()).Returns(auction);

            var result = _auctionService.UpdateAuctionBid(Guid.NewGuid(), newBid).Result;

            Assert.False(result);
        }
    }
}
