using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Net;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using JeftiniLetoviData.Services;

namespace JeftiniLetoviData.APICalls.Implementation
{
    public class FlightOffersAPICall : IFlightOffersAPICall
    {
        const string URL = "https://test.api.amadeus.com/v2/shopping/flight-offers";
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly IAuthenticationTokenAPICall _authentication;
        private readonly ILoggerClass _logger;
        public FlightOffersAPICall(IConfiguration configuration, IMemoryCache memoryCache, 
                                   IAuthenticationTokenAPICall authentication, ILoggerClass logger)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
            _authentication = authentication;
            _logger = logger;
        }

        public async Task<GetFlightOffersResponseDTO> GetFlightOffers(GetFlightOffersRequestDTO requestParameters)
        {
            string urlParameters = SetUrlParameters(requestParameters);
            string cacheKey = SetCacheKey(requestParameters);
            var flightOffers = new GetFlightOffersResponseDTO();

            try
            {
                flightOffers = _memoryCache.Get<GetFlightOffersResponseDTO>(cacheKey);
                if (flightOffers != null)
                    return flightOffers;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authentication.GetBearerToken());

                    HttpResponseMessage response = await client.GetAsync(urlParameters).ConfigureAwait(false);

                    if (!response.IsSuccessStatusCode)
                        throw new Exception(response.StatusCode.ToString());
                    
                    var jsonString = await response.Content.ReadAsStringAsync();
                    flightOffers = JsonConvert.DeserializeObject<GetFlightOffersResponseDTO>(jsonString);

                    if (flightOffers == null)
                        throw new Exception("Retrived data empty.");

                    _memoryCache.Set(cacheKey, flightOffers, TimeSpan.FromMinutes(10));
                    return flightOffers;
                }
            }
            catch(Exception ex)
            {
                _logger.WriteLog(ex.Message);
                return new GetFlightOffersResponseDTO();
            }
        }

        private string SetUrlParameters(GetFlightOffersRequestDTO requestParameters)
        {
            string urlParam = "";
            urlParam += "?originLocationCode=" + requestParameters.originLocationCode;
            urlParam += "&destinationLocationCode=" + requestParameters.destinationLocationCode;
            urlParam += "&departureDate=" + requestParameters.departureDate.ToString("yyyy-MM-dd");
            urlParam += "&returnDate=" + requestParameters.returnDate.ToString("yyyy-MM-dd");
            urlParam += "&adults=" + requestParameters.adults.ToString();
            urlParam += "&currencyCode=" + requestParameters.currencyCode;
            return urlParam;
        }

        private string SetCacheKey(GetFlightOffersRequestDTO requestParameters)
        {
            return requestParameters.originLocationCode + requestParameters.destinationLocationCode +
                   requestParameters.departureDate.ToString("yyyy-MM-dd") + requestParameters.returnDate.ToString("yyyy-MM-dd") +
                   requestParameters.adults.ToString() + requestParameters.currencyCode;
        }

        
    }
}
