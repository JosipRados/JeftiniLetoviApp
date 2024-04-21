using JeftiniLetoviData.DTO.FlightOffersDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeftiniLetoviData.DTO
{
    public class GetFlightOffersResponseDTO
    {
        public List<FlightOfferDTO> data { get; set; } = new List<FlightOfferDTO>();
    }
}
