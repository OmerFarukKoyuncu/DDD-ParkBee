using Microsoft.EntityFrameworkCore;
using ParkBee.Domain.Contracts;
using ParkBee.Domain.Entities;

namespace ParkBee.Infrastructure.Repositories
{
    public class ParkingSessionRepository : ISessionRepository
    {
        private readonly ApplicationDbContext _parkBeeDbContext;

        public ParkingSessionRepository(ApplicationDbContext parkBeeDbContext)
        {
            _parkBeeDbContext = parkBeeDbContext;
        }
        public async Task StartSession(ParkingSession sessionRequest, CancellationToken cancellationToken)
        {
            await _parkBeeDbContext.Sessions.AddAsync(sessionRequest, cancellationToken);
            await _parkBeeDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task StopSession(ParkingSession parkingSession, CancellationToken cancellationToken)
        {
            _parkBeeDbContext.Update(parkingSession);
            await _parkBeeDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<ParkingSession>> GetParkingSessionsByGarageId(Guid garageId, CancellationToken cancellationToken)
        {
            return await _parkBeeDbContext.Sessions.Where(x => x.GarageId == garageId && x.EndDate == null).ToListAsync();
        }

        public async Task<ParkingSession> GetParkingSessionsByUserId(Guid userId, CancellationToken cancellationToken)
        {
            return await _parkBeeDbContext.Sessions.Where(x => x.UserId == userId && x.EndDate == null).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> IsUserSessionExists(Guid userId, CancellationToken cancellationToken)
        {
            return await _parkBeeDbContext.Sessions.Where(x => x.UserId == userId && x.EndDate == null).AnyAsync(cancellationToken);
        }
    }
}