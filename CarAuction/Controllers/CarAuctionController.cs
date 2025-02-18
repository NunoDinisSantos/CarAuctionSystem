using CarAuction.Application.Services;
using CarAuction.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.Controllers
{ 
    [ApiController]
    [Route("api")]
    public class CarAuctionController : ControllerBase
    {
        private readonly IAuctionService _auctionService;
        private readonly IVehicleService _vehicleService;

        public CarAuctionController(IAuctionService auctionService, IVehicleService vehicleService)
        {
            _auctionService = auctionService;
            _vehicleService = vehicleService;
        }

        [HttpPost("auctions")]
        public async Task<IActionResult> CreateAuction([FromBody] CreateAuctionRequest createAuction)
        {
            var vehicle = _vehicleService.GetVehiclesById(createAuction.CarId).Result;

            if (vehicle == null)
            {
                return NotFound();
            }

            var result = await _auctionService.CreateAuction(vehicle, createAuction.EndDate);

            if(!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("auctions")]
        public async Task<IActionResult> GetAuctions()
        {
            var result = await _auctionService.GetAllAuctions();

            return Ok(result);
        }

        [HttpPut($"auctions/close/{{auctionId:guid}}")]
        public async Task<IActionResult> CloseAuction([FromRoute]Guid auctionId)
        {           
            var result = await _auctionService.UpdateAuctionActiveState(auctionId);

            if(result == null)
            {
                return NotFound();
            }

            await _vehicleService.DeleteVehicleById(result.CarId);

            return Ok(result);
        }

        [HttpPut($"auctions/bid/{{auctionId:guid}}/{{newBidValue:double}}")]
        public async Task<IActionResult> BidAuction([FromRoute] Guid auctionId, [FromRoute] double newBidValue)
        {
            var result = await _auctionService.UpdateAuctionBid(auctionId, newBidValue);

            if(!result)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}