using ParkBee.Domain.Entities;

namespace ParkBee.Domain.Contracts
{
    public interface IGarageRepository
    {
        Task<List<Garage>> GetGarages(CancellationToken cancellationToken);
        Task<Garage> GetGaragesById(Guid garageId, CancellationToken cancellationToken);
        Task<Guid> CreateGarage(Garage garage, CancellationToken cancellationToken);
    }
}
