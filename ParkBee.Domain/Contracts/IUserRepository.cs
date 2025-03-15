using ParkBee.Domain.Entities;

namespace ParkBee.Domain.Contracts
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers(CancellationToken cancellationToken);
        Task<bool> SearchUserById(Guid userId, CancellationToken cancellationToken);
        Task CreateUser(User user, CancellationToken cancellationToken);
        Task<List<User>> SearchUserByEmail(string email, CancellationToken cancellationToken);
    }
}
