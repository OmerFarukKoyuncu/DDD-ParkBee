using Microsoft.EntityFrameworkCore;
using ParkBee.Domain.Contracts;
using ParkBee.Domain.Entities;

namespace ParkBee.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _parkBeeDbContext;

        public UserRepository(ApplicationDbContext parkBeeDbContext)
        {
            _parkBeeDbContext = parkBeeDbContext;
        }
        public async Task<List<User>> GetUsers(CancellationToken cancellationToken)
        {
            return await _parkBeeDbContext.Users.ToListAsync(cancellationToken);
        }

        public async Task<List<User>> SearchUserByEmail(string email, CancellationToken cancellationToken)
        {
            return await _parkBeeDbContext.Users.Where(x=> x.Email == email).ToListAsync(cancellationToken);
        }
        public async Task<bool> SearchUserById(Guid userId, CancellationToken cancellationToken)
        {
            return await _parkBeeDbContext.Users.Where(x => x.Id == userId).AnyAsync(cancellationToken);
        }

        public async Task CreateUser(User user, CancellationToken cancellationToken)
        {
            await _parkBeeDbContext.Users.AddRangeAsync(user);
            await _parkBeeDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
