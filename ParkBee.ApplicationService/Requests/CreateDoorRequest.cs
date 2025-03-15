using ParkBee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkBee.Application.Requests
{
    public class CreateDoorRequest
    {
        public string Description { get; set; }

        public DoorType DoorType { get; set; }
        public string DoorIpAddress { get; set; }
    }
}
