using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeftiniLetoviData.DTO
{
    public class GetFlightOffersRequestDTO
    {
        public string? originLocationCode { get; set; }
        public string? destinationLocationCode { get; set; }
        public DateOnly departureDate { get; set; }
        public DateOnly returnDate { get; set; }
        public int adults { get; set; }
        public string? currencyCode { get; set; }
    }
}
