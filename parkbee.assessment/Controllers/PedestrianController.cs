using Microsoft.AspNetCore.Mvc;
using ParkBee.Api.Attributes;
using ParkBee.Application.Contracts;
using ParkBee.Application.Requests;
using ParkBee.Application.Responses;
using ParkBee.Domain.Constants;

namespace parkbee.assessment.Controllers
{
    [Route("pedestrian")]
    [ApiController]
    [Authorize]
    public class PedestrianController : ControllerBase
    {
        private readonly IPedestrianService _pedestrianService;

        public PedestrianController(IPedestrianService userService)
        {
            _pedestrianService = userService;
        }

        [HttpGet]
        public async Task<ResponseBase> IsPedestrianDoorReachable(Guid garageId, CancellationToken cancellationToken){
            return await _pedestrianService.isPedestrianDoorReachable(garageId, cancellationToken);
        }
    }
}