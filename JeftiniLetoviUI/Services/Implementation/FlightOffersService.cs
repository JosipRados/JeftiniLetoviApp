using JeftiniLetoviData.APICalls;
using JeftiniLetoviData.DTO;
using JeftiniLetoviUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeftiniLetoviUI.Services.Implementation
{
    public class FlightOffersService : IFlightOffersService
    {
        private readonly IFlightOffersAPICall _flightOffersAPICall;
        public FlightOffersService(IFlightOffersAPICall flightOffersAPICall)
        {
            _flightOffersAPICall = flightOffersAPICall;
        }

        public async Task<List<FlightOffersModel>> FetchFlightOffers(FlightOffersRequestModel request)
        {
            GetFlightOffersResponseDTO response = await _flightOffersAPICall.GetFlightOffers(FlightOffersRequestModelToDTO(request));
            if(response.data.Count <= 0 || response.data == null)
                return new List<FlightOffersModel>();

            return FlightOffersResponseDTOToModel(response);
            
        }

        private GetFlightOffersRequestDTO FlightOffersRequestModelToDTO(FlightOffersRequestModel request)
        {
            return new GetFlightOffersRequestDTO()
            {
                originLocationCode = request.DepartingCode,
                destinationLocationCode = request.ArrivalCode,
                departureDate = request.DepartureDate,
                returnDate = request.ReturningDate,
                adults = request.NumberOfPassengers,
                currencyCode = request.Currency
            };
        }

        private List<FlightOffersModel> FlightOffersResponseDTOToModel(GetFlightOffersResponseDTO response)
        {
            List<FlightOffersModel> flights = new List<FlightOffersModel>();

            foreach (var flight in response.data)
            {
                FlightOffersModel model = new FlightOffersModel()
                {
                    DepartureAirport = flight.itineraries.FirstOrDefault().segments.FirstOrDefault().departure.iataCode,
                    ArrivalAirport = flight.itineraries.FirstOrDefault().segments.LastOrDefault().arrival.iataCode,
                    DepartureDate = DateOnly.FromDateTime(flight.itineraries.FirstOrDefault().segments.FirstOrDefault().departure.at),
                    ReturnDate = DateOnly.FromDateTime(flight.itineraries.LastOrDefault().segments.LastOrDefault().arrival.at),
                    DepartureConnections = flight.itineraries.FirstOrDefault().segments.Count,
                    ReturnConnections = flight.itineraries.LastOrDefault().segments.Count,
                    NumberOfSeats = flight.numberOfBookableSeats,
                    Currency = flight.price.currency,
                    Price = flight.price.total

                };
                flights.Add(model);
            }

            return flights;
        }
    }
}
