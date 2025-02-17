using CarAuction.Application.Repository;
using CarAuction.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.Controllers
{ 
    [ApiController]
    [Route("api")]
    public class CarAuctionController : ControllerBase
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public CarAuctionController(IAuctionRepository auctionRepository, IVehicleRepository vehicleRepository)
        {
            _auctionRepository = auctionRepository;
            _vehicleRepository = vehicleRepository;
        }

        [HttpPost("auctions")]
        public async Task<IActionResult> CreateAuction([FromBody] CreateAuctionRequest createAuction)
        {
            var vehicle = _vehicleRepository.GetVehiclesById(createAuction.CarId).Result;

            if (vehicle == null)
            {
                return NotFound();
            }

            var result = await _auctionRepository.CreateAuction(vehicle, createAuction.EndDate);

            if(!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("auctions")]
        public async Task<IActionResult> GetAuctions()
        {
            var result = await _auctionRepository.GetAllAuctions();

            return Ok(result);
        }

        [HttpPut($"auctions/close/{{auctionId:guid}}")]
        public async Task<IActionResult> CloseAuction([FromRoute]Guid auctionId)
        {
            var auction = await _auctionRepository.GetAuctionById(auctionId);

            if (auction == null)
            {
                return NotFound();
            }

            if(!auction.isActive)
            {
                return BadRequest();
            }

            var result = await _auctionRepository.UpdateAuctionActiveState(auction);
            result &= await _vehicleRepository.DeleteVehicleById(auction.CarId);

            return Ok(result);
        }

        [HttpPut($"auctions/bid/{{auctionId:guid}}/{{newBidValue:double}}")]
        public async Task<IActionResult> BidAuction([FromRoute] Guid auctionId, [FromRoute] double newBidValue)
        {
            var auction = await _auctionRepository.GetAuctionById(auctionId);

            if (auction == null)
            {
                return NotFound();
            }

            var result = await _auctionRepository.UpdateAuctionBid(auction, newBidValue);

            if(result == false)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}