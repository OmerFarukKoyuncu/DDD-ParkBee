using Microsoft.EntityFrameworkCore;
using ParkBee.Domain.Contracts;
using ParkBee.Domain.Entities;

namespace ParkBee.Infrastructure.Repositories
{
    public class GarageRepository : IGarageRepository
    {
        private readonly ApplicationDbContext _parkBeeDbContext;

        public GarageRepository(ApplicationDbContext parkBeeDbContext)
        {
            _parkBeeDbContext = parkBeeDbContext;
        }

        public async Task<List<Garage>> GetGarages(CancellationToken cancellationToken)
        {
            return await _parkBeeDbContext.Garages.Include(x=>x.Doors).ToListAsync(cancellationToken);
        }

        public async Task<Garage> GetGaragesById(Guid garageId ,CancellationToken cancellationToken)
        {
            return await _parkBeeDbContext.Garages.Include(x=>x.Doors).FirstOrDefaultAsync(x => x.GarageId == garageId, cancellationToken);
        }

        public async Task<Guid> CreateGarage(Garage garage, CancellationToken cancellationToken)
        {
            await _parkBeeDbContext.Garages.AddRangeAsync(garage);
            await _parkBeeDbContext.SaveChangesAsync(cancellationToken);

            return garage.GarageId;
        }
    }
}
