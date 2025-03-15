

using parkbee.Application.Helper;
using ParkBee.Application.Requests;
using ParkBee.Domain.Constants;
using ParkBee.Domain.Contracts;
using ParkBee.Domain.Entities;

namespace ParkBee.Application.Contracts.Implementations
{
    public class ParkingSessionService : IParkingSessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IGarageRepository _garageRepository;
        private readonly IPingService _pingService;
        private readonly IUserService _userService;
        private readonly IGarageService _garageService;

        public ParkingSessionService(ISessionRepository sessionRepository, IGarageRepository garageRepository, IPingService pingService, IUserService userService, IGarageService garageService)
        {
            _sessionRepository = sessionRepository;
            _garageRepository = garageRepository;
            _pingService = pingService;
            _userService = userService;
            _garageService = garageService;
        }
        public async Task<ResponseBase> Start(StartParkingSessionRequest startParkingSessionRequest, CancellationToken cancellationToken)
        {
            var startSessionsResponse = new ResponseBase() { Success = true };

            var garages = await GetGarage(startParkingSessionRequest.GarageId, cancellationToken);

            await IsUserExists(startParkingSessionRequest.UserId, cancellationToken);

            await IsUserSessionExists(startParkingSessionRequest.UserId, cancellationToken);

            await CheckGarageCapacity(startParkingSessionRequest.GarageId, garages.Capacity, cancellationToken);

            var doorIpAddress = garages.Doors.First(x => x.DoorType == DoorType.Entry).DoorIpAddress;

            await IsDoorReachable(doorIpAddress);

            startSessionsResponse.Success = true;

            await _sessionRepository.StartSession(new ParkingSession()
            {
                UserId = startParkingSessionRequest.UserId,
                EndDate = startParkingSessionRequest.EndDate,
                CreatedDate = startParkingSessionRequest.CreatedDate,
                StartDate = startParkingSessionRequest.StartDate,
                GarageId = startParkingSessionRequest.GarageId
            }, cancellationToken);

            return startSessionsResponse;
        }

        public async Task<ResponseBase> Stop(Guid userId, Guid garageId, CancellationToken cancellationToken)
        {
            var startSessionsResponse = new ResponseBase() { Success = false };

            var garages = await GetGarage(garageId, cancellationToken);

            await IsUserExists(userId, cancellationToken);

            var doorIpAddress = garages.Doors.First(x => x.DoorType == DoorType.Exit).DoorIpAddress;

            await IsDoorReachable(doorIpAddress);

            startSessionsResponse.Success = true;

            var currentSession = await _sessionRepository.GetParkingSessionsByUserId(userId, cancellationToken);
            currentSession.EndDate = DateTime.UtcNow;

            await _sessionRepository.StopSession(currentSession, cancellationToken);

            return startSessionsResponse;
        }
        public async Task<List<ParkingSession>> GetParkingSessions(Guid garageId, CancellationToken cancellationToken)
        {
            return await _sessionRepository.GetParkingSessionsByGarageId(garageId, cancellationToken);
        }

        private async Task IsUserExists(Guid userId, CancellationToken cancellationToken)
        {
            var isUserExists = await _userService.SearchUserById(userId, cancellationToken);

            if (!isUserExists)
            {
                Helper.ThrowBusinessRuleException(ApplicationMessage.UserNotFound);
            }
        }

        private async Task IsDoorReachable(string doorIpAddress)
        {
            var response = new ResponseBase();

            var isDoorReachable = await _pingService.CheckGarageDoor(doorIpAddress);

            if (!isDoorReachable.Success)
            {
                Helper.ThrowBusinessRuleException(ApplicationMessage.FailedToAccessDoor);
            }
        }

        private async Task<Garage> GetGarage(Guid garageId, CancellationToken cancellationToken)
        {
            var garages = await _garageService.GetGarageById(garageId, cancellationToken);

            if (garages is null)
            {
                Helper.ThrowBusinessRuleException(ApplicationMessage.GarageNotFound);
            }

            return garages;
        }

        private async Task IsUserSessionExists(Guid userId, CancellationToken cancellationToken)
        {
            var currentSession = await _sessionRepository.IsUserSessionExists(userId, cancellationToken);

            if (currentSession)
            {
                Helper.ThrowBusinessRuleException(ApplicationMessage.UnFinishedSession);
            }

        }

        private async Task CheckGarageCapacity(Guid garageId, int garageCapacity, CancellationToken cancellationToken)
        {
            var parkingSessions = await GetParkingSessions(garageId, cancellationToken);

            if (parkingSessions.Any())
            {
                if (garageCapacity <= parkingSessions.Count)
                    Helper.ThrowBusinessRuleException(ApplicationMessage.NoPlaceInGarage);
            }
        }
    }
}
