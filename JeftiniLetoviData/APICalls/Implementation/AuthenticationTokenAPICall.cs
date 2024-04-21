using JeftiniLetoviData.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JeftiniLetoviData.APICalls.Implementation
{
    public class AuthenticationTokenAPICall : IAuthenticationTokenAPICall
    {
        const string URL = "https://test.api.amadeus.com/v1/security/oauth2/token";
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly ILoggerClass _logger;
        public AuthenticationTokenAPICall(IConfiguration configuration, IMemoryCache memoryCache, ILoggerClass logger)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<string> GetBearerToken()
        {
            string bearerToken = "";
            
            try
            {
                if (_memoryCache.TryGetValue<string>("BearerToken", out bearerToken))
                    return bearerToken;

                List<KeyValuePair<string, string>> urlParameters = SetParameters();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var request = new HttpRequestMessage(HttpMethod.Post, URL) { Content = new FormUrlEncodedContent(urlParameters) };
                    HttpResponseMessage response = await client.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                        throw new Exception(response.StatusCode.ToString());

                    var jsonString = await response.Content.ReadAsStringAsync();
                    var auth = JsonConvert.DeserializeObject<GetBearerTokenResponseDTO>(jsonString);

                    if (auth == null || auth.access_token == null)
                        throw new Exception("Retrived data empty.");

                    _memoryCache.Set("BearerToken", auth.access_token, TimeSpan.FromSeconds(auth.expires_in));
                    return auth.access_token;
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog("GetBearerToken, ERR: " + ex.Message);
                return "";
            }
        }

        private List<KeyValuePair<string, string>> SetParameters()
        {
            string? clientID = _configuration["ClientID"];
            string? clientSecret = _configuration["ClientSecret"];

            if (clientID == null || clientSecret == null)
                throw new Exception("ClientID and ClientSecret can't be retrived.");

            var param = new List<KeyValuePair<string, string>>();
            param.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            param.Add(new KeyValuePair<string, string>("client_id", clientID));
            param.Add(new KeyValuePair<string, string>("client_secret", clientSecret));
            return param;
        }
    }
}
