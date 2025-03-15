using ParkBee.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkBee.Application.Contracts
{
    public interface IPedestrianService
    {
        Task<ResponseBase> isPedestrianDoorReachable(Guid garageId, CancellationToken cancellationToken);
    }
}
