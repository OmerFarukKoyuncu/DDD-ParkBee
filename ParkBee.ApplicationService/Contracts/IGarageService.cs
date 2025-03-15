using ParkBee.Application.Requests;
using ParkBee.Application.Responses;
using ParkBee.Domain.Constants;
using ParkBee.Domain.Entities;

namespace ParkBee.Application.Contracts
{
    public interface IGarageService 
    {
        Task<List<GarageResponse>> GetGarages(CancellationToken cancellationToken);
        Task<ResponseBase<CreateGarageResponse>> CreateGarage(CreateGarageRequest createGarageApplicationRequest, CancellationToken cancellationToken);

        Task<Garage> GetGarageById(Guid garageId, CancellationToken cancellationToken);
    }
}
