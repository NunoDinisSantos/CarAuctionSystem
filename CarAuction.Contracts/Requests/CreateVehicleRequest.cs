using CarAuction.Contracts.Requests.VehicleTypes;
using System.Text.Json.Serialization;

namespace CarAuction.Contracts.Requests
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(CreateSedanRequest), "sedan")]
    [JsonDerivedType(typeof(CreateHatchbackRequest), "hatchback")]
    [JsonDerivedType(typeof(CreateSUVRequest), "suv")]
    [JsonDerivedType(typeof(CreateTruckRequest), "truck")]
    public class CreateVehicleRequest
    {
        public required string Manufacturer { get; set; } = string.Empty;

        public required string Model { get; set; } = string.Empty;

        public required int Year { get; set; }

        public required double StartingBid { get; set; }
    }
}