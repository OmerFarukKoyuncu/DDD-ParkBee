using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkBee.Application.Responses
{
    public interface ParkingSessionsResponse
    {
        public Guid UserId { get; set; }
        public Guid GarageId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }
    }
}
