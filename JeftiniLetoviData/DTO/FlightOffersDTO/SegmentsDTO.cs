using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeftiniLetoviData.DTO.FlightOffersDTO
{
    public class SegmentsDTO
    {
        public int id { get; set; }
        public FlightLocationTimeDTO departure { get; set; } = new FlightLocationTimeDTO();
        public FlightLocationTimeDTO arrival { get; set; } = new FlightLocationTimeDTO();
        public string? duration { get; set; }
    }
}
