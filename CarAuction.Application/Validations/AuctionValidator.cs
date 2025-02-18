using CarAuction.Application.Repository;
using CarAuction.Models.Auction;

namespace CarAuction.Application.Validations
{
    public class AuctionValidator
    {
        private readonly IAuctionRepository _auctionRepository;

        public AuctionValidator(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        public bool AuctionExists(Auction auction)
        {
            return _auctionRepository.ExistsById(auction.Id).Result;
        }

        public bool AuctionVehicleUnique(Auction auction)
        {
            return _auctionRepository.UniqueCarForAuction(auction.CarId).Result;
        }

        public bool AuctionValidDate(Auction auction)
        {
            return auction.EndDate >= DateTime.Now;
        }

        public bool AuctionValidBid(Auction auction, double bid)
        {
            return bid>auction.CurrentBid;
        }

        public bool AuctionIsOpen(Auction auction)
        {
            return _auctionRepository.GetAuctionById(auction.Id).Result!.isActive;
        }
    }
}