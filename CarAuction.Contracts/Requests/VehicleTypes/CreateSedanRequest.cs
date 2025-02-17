namespace CarAuction.Contracts.Requests.VehicleTypes
{
    public class CreateSedanRequest : CreateVehicleRequest
    {
        public int NumberOfDoors { get; set; }

    }
}
