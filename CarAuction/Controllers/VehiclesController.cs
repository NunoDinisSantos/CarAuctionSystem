using CarAuction.Api.Helper;
using CarAuction.Application.Repository;
using CarAuction.Contracts.Requests;
using CarAuction.Contracts.Requests.VehicleTypes;
using CarAuction.Models.Vehicle;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.Api.Controllers
{
    [ApiController]
    [Route("api")] 
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapCar _retriveCarObject;

        public VehiclesController(IVehicleRepository vehicleRepository, IMapCar retriveCarObject)
        {
            _vehicleRepository = vehicleRepository;
            _retriveCarObject = retriveCarObject;
        }
       
        [HttpPost("carinventory")]
        public IActionResult CreateVehicle([FromBody] CreateVehicleRequest newVehicleRequest)
        {
            var vehicleType = newVehicleRequest.GetType().Name;
            Vehicle vehicle;

            switch (vehicleType)
            {
                case "CreateSUVRequest":
                    vehicle = _retriveCarObject.MapVehicle((CreateSUVRequest)newVehicleRequest);
                    break;
                case "CreateTruckRequest":
                    vehicle = _retriveCarObject.MapVehicle((CreateTruckRequest)newVehicleRequest);
                    break;
                case "CreateHatchbackRequest":
                    vehicle = _retriveCarObject.MapVehicle((CreateHatchbackRequest)newVehicleRequest);
                    break;
                case "CreateSedanRequest":
                    vehicle = _retriveCarObject.MapVehicle((CreateSedanRequest)newVehicleRequest);
                    break;
                default:
                    vehicle = _retriveCarObject.MapVehicle(newVehicleRequest);
                    break;
            }

            var result = _vehicleRepository.CreateVehicle(vehicle).Result;

            if (!result)
            {
                return BadRequest();
            }
            
            return Created();
        }

        [HttpPost("carinventory/suv")]
        public IActionResult CreateSUV([FromBody]CreateSUVRequest newSuv)
        {          
            var vehicle = _retriveCarObject.MapVehicle(newSuv);

            var result = _vehicleRepository.CreateVehicle(vehicle).Result;

            if(!result)
            {
                return BadRequest();
            }

            return Created();
        }

        [HttpPost("carinventory/truck")]
        public IActionResult CreateTruck([FromBody] CreateTruckRequest newTruck)
        {
            var vehicle = _retriveCarObject.MapVehicle(newTruck);

            var result = _vehicleRepository.CreateVehicle(vehicle).Result;

            if (!result)
            {
                return BadRequest();
            }

            return Created();
        }

        [HttpPost("carinventory/sedan")]
        public IActionResult CreateSedan([FromBody] CreateSedanRequest newSedan)
        {
            var vehicle = _retriveCarObject.MapVehicle(newSedan);

            var result = _vehicleRepository.CreateVehicle(vehicle).Result;

            if (!result)
            {
                return BadRequest();
            }

            return Created();
        }

        [HttpPost("carinventory/hatchback")]
        public IActionResult CreateHatchback([FromBody] CreateHatchbackRequest newHatchback)
        {
            var vehicle = _retriveCarObject.MapVehicle(newHatchback);

            var result = _vehicleRepository.CreateVehicle(vehicle).Result;

            if (!result)
            {
                return BadRequest();
            }

            return Created();
        }

        [HttpGet("carinventory/type/{vehicleType}")]
        public IActionResult GetVehiclesByType([FromRoute] string vehicleType)
        {
            var result = _vehicleRepository.GetVehiclesByType(vehicleType);

            return Ok(result);
        }

        [HttpGet("carinventory/manufacturer/{manufacturer}")]
        public IActionResult GetVehiclesByManufacturer([FromRoute] string manufacturer)
        {
            var result = _vehicleRepository.GetVehiclesByManufacturer(manufacturer);

            return Ok(result);
        }

        [HttpGet($"carinventory/year/{{year:int}}")]
        public IActionResult GetVehiclesByYear([FromRoute] int year)
        {
            var result = _vehicleRepository.GetVehiclesByYear(year);

            return Ok(result);
        }

        [HttpGet("carinventory/model/{model}")]
        public IActionResult GetVehiclesByModel([FromRoute] string model)
        {
            var result = _vehicleRepository.GetVehiclesByModel(model);

            return Ok(result);
        }

        [HttpGet($"carinventory/{{id:guid}}")]
        public IActionResult GetVehiclesById([FromRoute] Guid id)
        {
            var result = _vehicleRepository.GetVehiclesById(id);

            return Ok(result);
        }
    }
}