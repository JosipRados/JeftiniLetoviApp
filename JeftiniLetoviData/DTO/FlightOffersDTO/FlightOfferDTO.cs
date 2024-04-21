using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeftiniLetoviData.DTO.FlightOffersDTO
{
    public class FlightOfferDTO
    {
        public int id { get; set; }
        public string? lastTicketingDate { get; set; }
        public int numberOfBookableSeats { get; set; }
        public List<ItinerariesDTO> itineraries { get; set; } = new List<ItinerariesDTO>();
        public PriceDTO price { get; set; } = new PriceDTO();
    }
}
