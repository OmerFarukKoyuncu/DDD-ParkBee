using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkBee.Domain.Entities
{
    public class Availability
    {
        public Guid Id { get; set; }
        public string GarageName { get; set; }
        public int AvailableSpaces { get; set; }
        public int Capacity { get; set; }
    }
}
