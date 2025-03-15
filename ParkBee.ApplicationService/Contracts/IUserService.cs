using ParkBee.Application.Requests;
using ParkBee.Application.Responses;
using ParkBee.Domain.Constants;
using ParkBee.Domain.Entities;

namespace ParkBee.Application.Contracts
{
    public interface IUserService
    {
        Task<ResponseBase<List<UserResponse>>> GetUsers(CancellationToken cancellationToken);
        Task<bool> SearchUserById(Guid userId, CancellationToken cancellationToken);
        Task<ResponseBase> CreateUser(CreateUserRequest createUserApplicationRequest, CancellationToken cancellationToken);
    }
}
