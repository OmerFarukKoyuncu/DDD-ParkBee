
using ParkBee.Domain.Entities;

namespace ParkBee.Application.Responses
{
    public class GarageResponse
    {
        public GarageResponse() {
        }
        public Guid GarageId { get; set; }

        public string GarageName { get; set; }

        public List<Door> Door { get; set; }
    }
}
