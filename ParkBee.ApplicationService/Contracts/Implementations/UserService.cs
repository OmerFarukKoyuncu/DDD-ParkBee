using ParkBee.Application.Requests;
using ParkBee.Application.Responses;
using ParkBee.Domain.Constants;
using ParkBee.Domain.Contracts;

namespace ParkBee.Application.Contracts.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResponseBase<List<UserResponse>>> GetUsers(CancellationToken cancellationToken)
        {
            var getUsers = new ResponseBase<List<UserResponse>>() { Success = true, MessageCode = ApplicationMessage.Success };

            var users = await _userRepository.GetUsers(cancellationToken);

            getUsers.Data = users.Select(x => new UserResponse()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                CreatedDate = x.CreatedDate,
            }).ToList();

            return getUsers;
        }

        public async Task<bool> SearchUserById(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.SearchUserById(userId, cancellationToken);

            return user;
        }

        public async Task<ResponseBase> CreateUser(CreateUserRequest createUserApplicationRequest, CancellationToken cancellationToken)
        {
            var response = new ResponseBase() { Success = true, MessageCode = ApplicationMessage.Success };

            var isUserExists = await _userRepository.SearchUserByEmail(createUserApplicationRequest.Email, cancellationToken);

            if (isUserExists.Count != 0)
            {
                response.UserMessage = ApplicationMessage.UserAlreadyExists.Message(createUserApplicationRequest.Email);
                response.Success = false;
                response.MessageCode = ApplicationMessage.Failed;

                return response;
            }

            await _userRepository.CreateUser(new Domain.Entities.User()
            {
                FirstName = createUserApplicationRequest.FirstName,
                LastName = createUserApplicationRequest.LastName,
                Email = createUserApplicationRequest.Email,
                PhoneNumber = createUserApplicationRequest.PhoneNumber,
                CreatedDate = DateTime.UtcNow,
                Id = Guid.NewGuid(),
            },cancellationToken);

            return response;
        }
    }
}
