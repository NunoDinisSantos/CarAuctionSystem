using CarAuction.Application.Repository;

namespace CarAuction.Tests.Repository
{
    public class VehicleRepositoryTests
    {
        private readonly VehicleRepository _vehicleRepository;

        public VehicleRepositoryTests()
        {
            _vehicleRepository = new VehicleRepository();
        }
    }
}
