using CarAuction.Models.Auction;

namespace CarAuction.Application.Repository
{
    internal class AuctionRepository : IAuctionRepository
    {
        public Task<bool> CreateAuction(Auction auction)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Auction>> GetAllAuctionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Auction?> GetAuctionByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAuctionAsync(Auction auction)
        {
            throw new NotImplementedException();
        }
    }
}
