using System.Text.Json.Serialization;

namespace CarAuction.Models.Vehicle
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(Sedan), "sedan")]
    [JsonDerivedType(typeof(Hatchback), "hatchback")]
    [JsonDerivedType(typeof(SUV), "suv")]
    [JsonDerivedType(typeof(Truck), "truck")]
    public class Vehicle
    {
        public required Guid Id { get; init; }

        public required string Type { get; set; } = string.Empty;

        public required string Manufacturer { get; set; } = string.Empty;

        public required string Model { get; set; } = string.Empty;

        public required int Year { get; set; }

        public required double StartingBid { get; set; }
    }
}