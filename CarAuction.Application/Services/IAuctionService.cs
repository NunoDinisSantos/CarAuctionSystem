using CarAuction.Models.Auction;
using CarAuction.Models.Vehicle;

namespace CarAuction.Application.Services
{
    public interface IAuctionService
    {
        Task<bool> CreateAuction(Vehicle vehicle, DateTime endDate);

        Task<Auction?> GetAuctionById(Guid id);

        Task<IEnumerable<Auction>> GetAllAuctions();

        Task<Auction?> UpdateAuctionActiveState(Guid id);

        Task<bool> UpdateAuctionBid(Guid auctionId, double newBid);
    }
}
