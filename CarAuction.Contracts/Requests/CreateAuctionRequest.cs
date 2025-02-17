namespace CarAuction.Contracts.Requests
{
    public class CreateAuctionRequest
    {
        public required Guid CarId { get; init; }

        public required DateTime EndDate { get; init; }
    }
}