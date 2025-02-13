namespace CarAuction.Models.Vehicle
{
    public class Vehicle
    {
        public required int Id { get; set; }

        public required string Type { get; set; }

        public required string Manufacturer { get; set; } = string.Empty;

        public required string Model { get; set; } = string.Empty;

        public required int Year { get; set; }

        public required double StartingBid { get; set; }
    }
}
