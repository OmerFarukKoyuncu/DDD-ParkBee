using Microsoft.AspNetCore.Mvc;
using ParkBee.Api.Attributes;
using ParkBee.Application.Contracts;
using ParkBee.Application.Requests;
using ParkBee.Application.Responses;
using ParkBee.Domain;
using ParkBee.Domain.Constants;
using ParkBee.Domain.Entities;

namespace parkbee.assessment.Controllers
{
    [Route("garages")]
    [ApiController]
    [Authorize]
    public class GaragesController : ControllerBase
    {
        private readonly IGarageService _garageService;

        public GaragesController(IGarageService garageService)
        {
            _garageService = garageService;
        }

        [HttpGet]
        public async Task<List<GarageResponse>> GetGarages(CancellationToken cancellationToken){
            return await _garageService.GetGarages(cancellationToken);
        }

        [HttpPost]
        public async Task<ResponseBase<CreateGarageResponse>> CreateGarage(CreateGarageRequest createGarageApplicationRequest, CancellationToken cancellationToken)
        {
            return await _garageService.CreateGarage(createGarageApplicationRequest, cancellationToken);
        }
    }
}