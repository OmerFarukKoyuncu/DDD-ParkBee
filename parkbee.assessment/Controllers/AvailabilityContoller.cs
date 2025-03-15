using Microsoft.AspNetCore.Mvc;
using ParkBee.Api.Attributes;
using ParkBee.Application.Contracts;
using ParkBee.Application.Responses;
using ParkBee.Domain.Constants;

namespace parkbee.assessment.Controllers
{
    [Route("availability")]
    [ApiController]
    [Authorize]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;

        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [HttpGet]
        public async Task<ResponseBase<List<AvailabilityResponse>>> GetAvailability(Guid garageId, CancellationToken cancellationToken)
        {
            return await _availabilityService.GetAvailability(garageId, cancellationToken);
        }
    }
}
