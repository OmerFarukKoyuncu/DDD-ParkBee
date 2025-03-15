using ParkBee.Domain.Entities;

namespace ParkBee.Domain.Contracts
{
    public interface ISessionRepository
    {
        Task StartSession(ParkingSession sessionRequest, CancellationToken cancellationToken);
        Task StopSession(ParkingSession userId, CancellationToken cancellationToken);
        Task<List<ParkingSession>> GetParkingSessionsByGarageId(Guid garageId, CancellationToken cancellationToken);
        Task<ParkingSession> GetParkingSessionsByUserId(Guid userId, CancellationToken cancellationToken);
        Task<bool> IsUserSessionExists(Guid userId, CancellationToken cancellationToken);
    }
}
