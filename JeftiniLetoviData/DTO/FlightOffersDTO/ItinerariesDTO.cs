using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeftiniLetoviData.DTO.FlightOffersDTO
{
    public class ItinerariesDTO
    {
        public string? duration { get; set; }
        public List<SegmentsDTO> segments { get; set; } = new List<SegmentsDTO>();
    }
}
