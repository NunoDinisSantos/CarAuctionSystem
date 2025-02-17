using CarAuction.Models.Auction;

namespace CarAuction.Application.Validations
{
    public class AuctionValidator
    {
       public bool AuctionExists(Auction auction, List<Auction> fakeDb)
        {
            return !fakeDb.Any(x => x.Id == auction.Id);
        }

        public bool AuctionVehicleUnique(Auction auction, List<Auction> fakeDb)
        {
            return !fakeDb.Any(x => x.CarId == auction.CarId);
        }

        public bool AuctionValidDate(Auction auction)
        {
            return auction.EndDate >= DateTime.Now;
        }

        public bool AuctionValidBid(Auction auction, double bid)
        {
            return bid>auction.CurrentBid;
        }
    }
}