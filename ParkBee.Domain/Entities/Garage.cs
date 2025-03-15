
namespace ParkBee.Domain.Entities
{
    public class Garage
    {
        public Guid GarageId { get; set; }

        public string Name { get; set; }
        public int Capacity { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<Door> Doors { get; set; }
    }
}