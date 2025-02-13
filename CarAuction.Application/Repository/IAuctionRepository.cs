using CarAuction.Models.Auction;

namespace CarAuction.Application.Repository
{
    internal interface IAuctionRepository
    {
        Task<bool> CreateAuction(Auction auction);

        Task<Auction?> GetAuctionByIdAsync(int id);

        Task<IEnumerable<Auction>> GetAllAuctionsAsync();

        Task<bool> UpdateAuctionAsync(Auction auction);
    }
}
