using ParkBee.Application.Requests;
using ParkBee.Application.Responses;
using ParkBee.Domain.Constants;
using ParkBee.Domain.Entities;

namespace ParkBee.Application.Contracts
{
    public interface IParkingSessionService
    {
        Task<ResponseBase> Start(StartParkingSessionRequest startParkingSessionRequest, CancellationToken cancellationToken);
        Task<ResponseBase> Stop(Guid userId, Guid garageId, CancellationToken cancellationToken);
        Task<List<ParkingSession>> GetParkingSessions(Guid garageId, CancellationToken cancellationToken);
    }
}
