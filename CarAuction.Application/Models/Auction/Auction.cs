namespace CarAuction.Models.Auction
{
    public class Auction
    {
        public Guid Id { get; init; }

        public Guid CarId { get; init; }

        public double CurrentBid { get; set; }

        public DateTime EndDate { get; set; }

        public bool isActive { get; set; } = true;
    }
}