using parkbee.Application.Helper;
using ParkBee.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParkBee.Application.Contracts.Implementations
{
    public class PedestrianService : IPedestrianService
    {
        private readonly IGarageService _garageService;
        private readonly IPingService _pingService;
        public PedestrianService(IGarageService garageService, IPingService pingService) {
            _garageService = garageService;
            _pingService = pingService;
        }

        public async Task<ResponseBase> isPedestrianDoorReachable(Guid garageId, CancellationToken cancellationToken)
        {
            var response = new ResponseBase() { Success = true };

            var garages = await _garageService.GetGarageById(garageId, cancellationToken);

            if (garages is null)
            {
                Helper.ThrowBusinessRuleException(ApplicationMessage.GarageNotFound);
            }

            var doorIpAddress = garages.Doors.First(x => x.DoorType == Domain.Entities.DoorType.Pedestrian).DoorIpAddress;

            var isDoorReachable = await _pingService.CheckGarageDoor(doorIpAddress);

            if (!isDoorReachable.Success)
            {
                response.Success = false;

                return response;
            }

            return response;
        }
    }
}
