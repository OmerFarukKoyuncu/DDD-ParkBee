using ParkBee.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkBee.Application.Contracts
{
    public interface IPingService
    {
        Task<ResponseBase> CheckGarageDoor(string DoorIpAddress);
    }
}
