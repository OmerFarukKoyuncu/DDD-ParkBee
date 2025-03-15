using Microsoft.AspNetCore.Mvc;
using ParkBee.Api.Attributes;
using ParkBee.Application.Contracts;
using ParkBee.Application.Requests;
using ParkBee.Application.Responses;
using ParkBee.Domain.Constants;

namespace parkbee.assessment.Controllers
{
    [Route("users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ResponseBase> CreateUser(CreateUserRequest createUserApplicationRequest, CancellationToken cancellationToken)
        {
            return await _userService.CreateUser(createUserApplicationRequest, cancellationToken);
        }
    }
}