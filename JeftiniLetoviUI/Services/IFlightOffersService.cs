using JeftiniLetoviData.DTO;
using JeftiniLetoviUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeftiniLetoviUI.Services
{
    public interface IFlightOffersService
    {
        public Task<List<FlightOffersModel>> FetchFlightOffers(FlightOffersRequestModel request);
    }
}
