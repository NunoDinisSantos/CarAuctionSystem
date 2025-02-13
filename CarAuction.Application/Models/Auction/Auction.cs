namespace CarAuction.Models.Auction
{
    public class Auction
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public int UserId { get; set; }

        public double CurrentBid { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsOpen { get; set; }
    }
}
