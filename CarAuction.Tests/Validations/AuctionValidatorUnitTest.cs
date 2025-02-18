using CarAuction.Application.Repository;
using CarAuction.Application.Services;
using CarAuction.Application.Validations;
using CarAuction.Models.Auction;
using NSubstitute;

namespace CarAuction.Tests.Validations
{
    public class AuctionValidatorUnitTest
    {
        private readonly AuctionValidator _auctionValidator;
        private readonly AuctionService _auctionService;
        private readonly IAuctionRepository _auctionRepository = Substitute.For<IAuctionRepository>();

        public AuctionValidatorUnitTest()
        {
            _auctionValidator = new AuctionValidator(_auctionRepository);
            _auctionService = new AuctionService(_auctionRepository, _auctionValidator);
        }

        [Fact]      
        public void AuctionVehicleUnique_ShouldNotCreateAuction_WhenCarAlreadyIsAuctioned()
        {
            var usedGuid = Guid.NewGuid();

            var auction = new Auction()
            {
                Id = Guid.NewGuid(),
                CarId = usedGuid,
                CurrentBid = 15000,
                EndDate = DateTime.Now,
                isActive = true,
            };

            var result = _auctionValidator.AuctionVehicleUnique(auction);

            Assert.False(result);
        }

        [Fact]
        public void AuctionVehicleUnique_ShouldCreateAuction_WhenCarIsNotAuctioned()
        {
            var auction = new Auction()
            {
                Id = Guid.NewGuid(),
                CarId = Guid.NewGuid(),
                CurrentBid = 15000,
                EndDate = DateTime.Now,
                isActive = true,
            };

            _auctionRepository.UniqueCarForAuction(Arg.Any<Guid>()).Returns(true);

            var result = _auctionValidator.AuctionVehicleUnique(auction);

            Assert.True(result);
        }

        [Fact]
        public void AuctionValidDate_ShouldCreateAuction_WhenDateIsValid()
        {
            var auction = new Auction()
            {
                Id = Guid.NewGuid(),
                CarId = Guid.NewGuid(),
                CurrentBid = 15000,
                EndDate = new DateTime(9999, 12, 31),
                isActive = true,
            };

            var result = _auctionValidator.AuctionValidDate(auction);

            Assert.True(result);
        }

        [Fact]
        public void AuctionValidDate_ShouldNotCreateAuction_WhenDateIsNotValid()
        {
            var auction = new Auction()
            {
                Id = Guid.NewGuid(),
                CarId = Guid.NewGuid(),
                CurrentBid = 15000,
                EndDate = new DateTime(1900, 12, 31),
                isActive = true,
            };

            var result = _auctionValidator.AuctionValidDate(auction);

            Assert.False(result);
        }

        [Fact]
        public void AuctionExists_ShouldNotCreateAuction_WhenAuctionExistWithId()
        {
            var usedGuid = Guid.NewGuid();

            var auction = new Auction()
            {
                CarId = Guid.NewGuid(),
                Id = usedGuid,
                CurrentBid = 15000,
                EndDate = DateTime.Now,
                isActive = true,
            };

            var result = _auctionValidator.AuctionExists(auction);

            Assert.False(result);
        }

        [Fact]
        public void AuctionExists_ShouldCreateAuction_WhenAuctionNotExistWithId()
        {
            var auction = new Auction()
            {
                CarId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                CurrentBid = 15000,
                EndDate = DateTime.Now,
                isActive = true,
            };

            var canCreate = !_auctionValidator.AuctionExists(auction);

            Assert.True(canCreate);
        }

        [Fact]
        public void AuctionValidBid_ShouldBeAbleToBid_WhenBidIsGreaterThanCurrentBid()
        {
            var auction = new Auction()
            {
                Id = Guid.NewGuid(),
                CarId = Guid.NewGuid(),
                CurrentBid = 15000,
                EndDate = new DateTime(1900, 12, 31),
                isActive = true,
            };

            var bid = 20000;

            var result = _auctionValidator.AuctionValidBid(auction, bid);

            Assert.True(result);
        }

        [Fact]
        public void AuctionValidBid_ShouldNotBeAbleToBid_WhenBidIsLessThanCurrentBid()
        {
            var auction = new Auction()
            {
                Id = Guid.NewGuid(),
                CarId = Guid.NewGuid(),
                CurrentBid = 15000,
                EndDate = new DateTime(1900, 12, 31),
                isActive = true,
            };

            var bid = 14000;

            var result = _auctionValidator.AuctionValidBid(auction, bid);

            Assert.False(result);
        }
    }
}