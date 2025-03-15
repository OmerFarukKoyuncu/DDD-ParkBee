
namespace ParkBee.Application.Responses
{
    public class AvailabilityResponse
    {   
        public Guid GarageId { get; set; }
        public string GarageName { get; set; }
        public int AvailableSpaces { get; set; }
        public int Capacity { get; set; }
    }
}
