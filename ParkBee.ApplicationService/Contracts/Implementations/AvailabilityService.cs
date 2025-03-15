using ParkBee.Application.Responses;
using ParkBee.Domain.Constants;
using ParkBee.Domain.Contracts;

namespace ParkBee.Application.Contracts.Implementations
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IParkingSessionService _parkingSessionService;
        private readonly IGarageRepository _garageRepository;

        public AvailabilityService(IParkingSessionService parkingSessionService, IGarageRepository garageRepository)
        {
            _parkingSessionService = parkingSessionService;
            _garageRepository = garageRepository;
        }

        public async Task<ResponseBase<List<AvailabilityResponse>>> GetAvailability(Guid garageId, CancellationToken cancellationToken)
        {
            var getAvailabilityResponse = new ResponseBase<List<AvailabilityResponse>>() { Success = true, Message = ApplicationMessage.UserMessage(ApplicationMessage.EmptyPlaceInGarage), MessageCode = ApplicationMessage.Success };

            var availability = new List<AvailabilityResponse>();

            var garages = await _garageRepository.GetGaragesById(garageId, cancellationToken);
           
            var parkingSessions = await _parkingSessionService.GetParkingSessions(garageId, cancellationToken);

            if (parkingSessions.Any())
            {
                if (garages.Capacity > parkingSessions.Count)
                {
                    availability.Add(new AvailabilityResponse
                    {
                        AvailableSpaces = garages.Capacity - parkingSessions.Count,
                        Capacity = garages.Capacity,
                        GarageId = garageId,
                        GarageName = garages.Name
                    });

                    getAvailabilityResponse.MessageCode = ApplicationMessage.EmptyPlaceInGarage;
                    getAvailabilityResponse.Message = ApplicationMessage.UserMessage(ApplicationMessage.EmptyPlaceInGarage);
                    getAvailabilityResponse.Data = availability;
                }
                else
                {
                    getAvailabilityResponse.MessageCode = ApplicationMessage.NoPlaceInGarage;
                    getAvailabilityResponse.Message = ApplicationMessage.UserMessage(ApplicationMessage.NoPlaceInGarage);
                }

                return getAvailabilityResponse;
            }

            availability.Add(new AvailabilityResponse
            {
                AvailableSpaces = garages.Capacity,
                Capacity = garages.Capacity,
                GarageId = garageId,
                GarageName = garages.Name
            });

            getAvailabilityResponse.Data = availability;

            return getAvailabilityResponse;
        }
    }
}
