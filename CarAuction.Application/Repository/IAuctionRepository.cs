using CarAuction.Models.Auction;

namespace CarAuction.Application.Repository
{
    public interface IAuctionRepository
    {
        Task<bool> CreateAuction(Auction auctionToCreate);

        Task<Auction?> GetAuctionById(Guid id);

        Task<IEnumerable<Auction>> GetAllAuctions();

        Task<bool> UpdateAuctionActiveState(Auction auction);

        Task<bool> UpdateAuctionBid(Auction auction);

        Task<bool> ExistsById(Guid id);

        Task<bool> UniqueCarForAuction(Guid id);
    }
}