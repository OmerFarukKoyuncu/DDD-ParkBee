namespace ParkBee.Domain.Entities
{
    public class ParkingSession
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid GarageId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
    }
}
