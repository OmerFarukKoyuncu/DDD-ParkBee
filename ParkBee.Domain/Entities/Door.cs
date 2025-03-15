namespace ParkBee.Domain.Entities
{
    public class Door
    {
        public Guid DoorId { get; set; }
        public Guid GarageId { get; set; }

        public string Description { get; set; }

        public DoorType DoorType { get; set; }
        public string DoorIpAddress { get; set; }
    }
}
