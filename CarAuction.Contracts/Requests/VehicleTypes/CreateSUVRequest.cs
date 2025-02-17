namespace CarAuction.Contracts.Requests.VehicleTypes
{
    public class CreateSUVRequest : CreateVehicleRequest
    {
        public int NumberOfSeats { get; set; }
    }
}
