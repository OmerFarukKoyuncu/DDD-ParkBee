using Microsoft.AspNetCore.Mvc;
using ParkBee.Api.Attributes;
using ParkBee.Application.Contracts;
using ParkBee.Application.Requests;
using ParkBee.Domain.Constants;

namespace parkbee.assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParkingSessionsController : ControllerBase
    {
        private readonly IParkingSessionService _parkingSessionService;

        public ParkingSessionsController(IParkingSessionService parkingSessionService)
        {
            _parkingSessionService = parkingSessionService;
        }

        [HttpPost("start")]
        public async Task<ResponseBase> Start(ParkingStartSessionRequest sessionRequest, CancellationToken cancellationToken)
        {
            return await _parkingSessionService.Start(new StartParkingSessionRequest()
            {
                UserId = sessionRequest.UserId,
                StartDate = sessionRequest.StartDate,
                GarageId = sessionRequest.GarageId,
                CreatedDate = sessionRequest.CreatedDate,
                EndDate = null
            }, cancellationToken);
        }

        [HttpPost("stop")]
        public async Task<ResponseBase> Stop(Guid userId, Guid garageId, CancellationToken cancellationToken)
        {
            return await _parkingSessionService.Stop(userId, garageId, cancellationToken);
        }
    }
}