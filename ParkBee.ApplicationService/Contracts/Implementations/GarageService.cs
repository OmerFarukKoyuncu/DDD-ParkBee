using ParkBee.Application.Requests;
using ParkBee.Application.Responses;
using ParkBee.Domain.Constants;
using ParkBee.Domain.Contracts;
using ParkBee.Domain.Entities;

namespace ParkBee.Application.Contracts.Implementations
{
    public class GarageService : IGarageService
    {
        private readonly IGarageRepository _garageRepository;

        public GarageService(IGarageRepository garageRepository)
        {
            _garageRepository = garageRepository;
        }
        public async Task<List<GarageResponse>> GetGarages(CancellationToken cancellationToken)
        {
            var garages = await _garageRepository.GetGarages(cancellationToken);

            return garages.Select(x => new GarageResponse()
            {
                GarageId = x.GarageId,
                GarageName = x.Name,
                Door = x.Doors
            }).ToList();
        }

        public async Task<ResponseBase<CreateGarageResponse>> CreateGarage(CreateGarageRequest createGarageRequest, CancellationToken cancellationToken)
        {
            var responseCreateGarage = new ResponseBase<CreateGarageResponse>() { Success = true, Data = new CreateGarageResponse() };

            var garageId = await _garageRepository.CreateGarage(new Garage()
            {
                GarageId = new Guid(),
                Capacity = createGarageRequest.Capacity,
                Doors = createGarageRequest.Door,
                CreatedDate = createGarageRequest.CreatedDate,
                Name = createGarageRequest.Name,
            }, cancellationToken);

            responseCreateGarage.Data.GarageId = garageId;
            return responseCreateGarage;
        }

        public async Task<Garage> GetGarageById(Guid garageId, CancellationToken cancellationToken)
        {
            return await _garageRepository.GetGaragesById(garageId, cancellationToken);
        }
    }
}
