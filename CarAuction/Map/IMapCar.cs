using CarAuction.Contracts.Requests;
using CarAuction.Contracts.Requests.VehicleTypes;
using CarAuction.Models.Vehicle;

namespace CarAuction.Api.Helper
{
    public interface IMapCar
    {
        public Vehicle MapVehicle(CreateSUVRequest suv);
        public Vehicle MapVehicle(CreateHatchbackRequest hatchback);
        public Vehicle MapVehicle(CreateSedanRequest sedan);
        public Vehicle MapVehicle(CreateTruckRequest truck);
        public Vehicle MapVehicle(CreateVehicleRequest _vehicle);
    }
}
