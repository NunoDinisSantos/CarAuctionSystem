namespace CarAuction.Contracts.Requests.VehicleTypes
{
    public class CreateHatchbackRequest : CreateVehicleRequest
    {
        public int NumberOfDoors { get; set; }
    }
}
