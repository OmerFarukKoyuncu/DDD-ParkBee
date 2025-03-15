﻿namespace ParkBee.Application.Requests
{
    public class ParkingStartSessionRequest
    {
        public Guid UserId { get; set; }
        public Guid GarageId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime StartDate { get; set; }

    }
}
