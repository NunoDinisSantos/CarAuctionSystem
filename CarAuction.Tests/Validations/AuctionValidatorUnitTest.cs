using CarAuction.Application.Validations;
using CarAuction.Models.Auction;

namespace CarAuction.Tests.Validations
{
    public class AuctionValidatorUnitTest
    {
        private AuctionValidator _auctionValidator = new();

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

            var fakeDb = new List<Auction>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    CarId = usedGuid,
                    CurrentBid = 18000,
                    EndDate = DateTime.Now,
                    isActive = true,
                }
            };

            var result = _auctionValidator.AuctionVehicleUnique(auction, fakeDb);

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

            var fakeDb = new List<Auction>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    CarId = Guid.NewGuid(),
                    CurrentBid = 18000,
                    EndDate = DateTime.Now,
                    isActive = true,
                }
            };

            var result = _auctionValidator.AuctionVehicleUnique(auction, fakeDb);

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

            var fakeDb = new List<Auction>()
            {
                new()
                {
                    CarId = Guid.NewGuid(),
                    Id = usedGuid,
                    CurrentBid = 18000,
                    EndDate = DateTime.Now,
                    isActive = true,
                }
            };

            var result = _auctionValidator.AuctionExists(auction, fakeDb);

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

            var fakeDb = new List<Auction>()
            {
                new()
                {
                    CarId = Guid.NewGuid(),
                    Id = Guid.NewGuid(),
                    CurrentBid = 18000,
                    EndDate = DateTime.Now,
                    isActive = true,
                }
            };

            var result = _auctionValidator.AuctionExists(auction, fakeDb);

            Assert.True(result);
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