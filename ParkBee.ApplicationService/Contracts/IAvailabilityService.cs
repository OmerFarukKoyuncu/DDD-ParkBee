using ParkBee.Application.Responses;
using ParkBee.Domain.Constants;

namespace ParkBee.Application.Contracts
{
    public interface IAvailabilityService
    {
        Task<ResponseBase<List<AvailabilityResponse>>> GetAvailability(Guid garageId, CancellationToken cancellationToken);
    }
}
