using CarAuction.Models.Auction;
using CarAuction.Models.Vehicle;

namespace CarAuction.Application.Repository
{
    public interface IAuctionRepository
    {
        Task<bool> CreateAuction(Vehicle vehicle, DateTime endDate);

        Task<Auction?> GetAuctionById(Guid id);

        Task<IEnumerable<Auction>> GetAllAuctions();

        Task<bool> UpdateAuctionState(Auction auction);

        Task<bool> UpdateAuctionBid(Auction auction, double newBid);
    }
}