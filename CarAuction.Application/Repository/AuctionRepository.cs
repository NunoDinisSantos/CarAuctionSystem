using CarAuction.Models.Auction;

namespace CarAuction.Application.Repository
{
    public class AuctionRepository : IAuctionRepository
    {
        // In memory database
        private readonly List<Auction> _auctions = new();

        public Task<bool> CreateAuction(Auction auctionToCreate)
        {          
            _auctions.Add(auctionToCreate);
            
            return Task.FromResult(true);
        }

        public Task<bool> ExistsById(Guid id)
        {
            return Task.FromResult(_auctions.Any(x => x.Id == id));
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

        public Task<bool> UniqueCarForAuction(Guid id)
        {
            return Task.FromResult(!_auctions.Any(x => x.CarId == id));
        }

        public Task<bool> UpdateAuctionActiveState(Auction auction)
        {
            _auctions[_auctions.FindIndex(x => x.Id == auction.Id)] = auction;

            return Task.FromResult(true);
        }

        public Task<bool> UpdateAuctionBid(Auction auction)
        {
            _auctions.FirstOrDefault(x => x.Id == auction.Id)!.CurrentBid = auction.CurrentBid;
            return Task.FromResult(true);
        }
    }
}