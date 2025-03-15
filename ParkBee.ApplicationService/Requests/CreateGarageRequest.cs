using ParkBee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkBee.Application.Requests
{
    public class CreateGarageRequest
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Door> Door { get; set; }
    }
}
