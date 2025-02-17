namespace CarAuction.Contracts.Requests.VehicleTypes
{
    public class CreateTruckRequest : CreateVehicleRequest
    {
        public double LoadCapacity { get; set; }

    }
}
