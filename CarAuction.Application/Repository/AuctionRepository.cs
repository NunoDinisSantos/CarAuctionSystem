using CarAuction.Application.Validations;
using CarAuction.Models.Auction;
using CarAuction.Models.Vehicle;

namespace CarAuction.Application.Repository
{
    public class AuctionRepository : IAuctionRepository
    {
        // In memory database
        private readonly List<Auction> _auctions = new();

        private AuctionValidator _validator = new();

        public Task<bool> CreateAuction(Vehicle vehicle, DateTime endDate)
        {
            var auction = new Auction()
            {
                Id = Guid.NewGuid(),
                CarId = vehicle.Id,
                CurrentBid = vehicle.StartingBid,
                EndDate = endDate,
                isActive = true,
            };

            var result = _validator.AuctionExists(auction, _auctions);
            result &= _validator.AuctionValidDate(auction);
            result &= _validator.AuctionVehicleUnique(auction, _auctions);

            if (result)
            {
                _auctions.Add(auction);
            }

            return Task.FromResult(result);
        }

        public Task<IEnumerable<Auction>> GetAllAuctions()
        {
            var auctions = _auctions.AsEnumerable();
            return Task.FromResult(auctions);
        }

        public Task<Auction?> GetAuctionById(Guid id)
        {
            var auctions = _auctions.Where(x => x.Id == id).FirstOrDefault();

            return Task.FromResult(auctions);
        }

        public Task<bool> UpdateAuctionState(Auction auction)
        {
            _auctions[_auctions.FindIndex(x => x.Id == auction.Id)] = auction;

            return Task.FromResult(true);
        }

        public Task<bool> UpdateAuctionBid(Auction auction, double newBid)
        {
            var result = _validator.AuctionValidDate(auction);
            result &= _validator.AuctionValidBid(auction, newBid);

            if (!result)
            {
                return Task.FromResult(result);
            }

            _auctions.FirstOrDefault(x => x.Id == auction.Id)!.CurrentBid = newBid;
            return Task.FromResult(result);
        }
    }
}