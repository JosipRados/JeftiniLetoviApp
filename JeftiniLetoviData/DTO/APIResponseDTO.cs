using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeftiniLetoviData.DTO
{
    public class APIResponseDTO
    {
        public bool isSuccess { get; set; } = false;
        public string message { get; set; } = "";
        public GetFlightOffersResponseDTO data { get; set; } = new GetFlightOffersResponseDTO();
    }
}
