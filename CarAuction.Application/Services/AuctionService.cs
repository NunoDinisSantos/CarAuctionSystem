using CarAuction.Application.Repository;
using CarAuction.Application.Validations;
using CarAuction.Models.Auction;
using CarAuction.Models.Vehicle;

namespace CarAuction.Application.Services
{
    public class AuctionService : IAuctionService
    {
        private AuctionValidator _validator;
        private IAuctionRepository _repository;

        public AuctionService(IAuctionRepository repository, AuctionValidator auctionValidator)
        {
            _repository = repository;
            _validator = auctionValidator;
        }

        public Task<bool> CreateAuction(Vehicle vehicle, DateTime endDate)
        {
            var auction = new Auction()
            {
                Id = Guid.NewGuid(),
                CarId = vehicle.Id,
                CurrentBid = vehicle.StartingBid,
                EndDate = endDate,
                isActive = true,
            };

            var result = !_validator.AuctionExists(auction);
            result &= _validator.AuctionValidDate(auction);
            result &= _validator.AuctionVehicleUnique(auction);

            if (!result)
            {
                return Task.FromResult(result);
            }

            result = _repository.CreateAuction(auction).Result;

            return Task.FromResult(result);
        }

        public Task<IEnumerable<Auction>> GetAllAuctions()
        {
            return _repository.GetAllAuctions();
        }

        public Task<Auction?> GetAuctionById(Guid id)
        {
            return _repository.GetAuctionById(id);
        }

        public Task<Auction?> UpdateAuctionActiveState(Guid id)
        {
            var auction = _repository.GetAuctionById(id).Result;

            if(auction == null)
            {
                return Task.FromResult(auction);
            }

            auction.isActive = false;
            _repository.UpdateAuctionActiveState(auction);

            return Task.FromResult(auction)!;
        }

        public Task<bool> UpdateAuctionBid(Guid auctionId, double newBid)
        {
            var auction = _repository.GetAuctionById(auctionId).Result;

            if (auction == null)
            {
                return Task.FromResult(false);
            }

            var result = _validator.AuctionValidDate(auction);
            result &= _validator.AuctionValidBid(auction, newBid);
            result &= _validator.AuctionIsOpen(auction);

            if (!result)
            {
                return Task.FromResult(false);
            }

            auction.CurrentBid = newBid;
            _repository.UpdateAuctionBid(auction);

            return Task.FromResult(true)!;
        }
    }
}