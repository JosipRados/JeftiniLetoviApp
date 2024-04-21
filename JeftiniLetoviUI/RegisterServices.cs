using JeftiniLetoviUI.Services;
using JeftiniLetoviUI.Services.Implementation;
using JeftiniLetoviData.APICalls;
using JeftiniLetoviData.APICalls.Implementation;
using JeftiniLetoviData.Services;
using JeftiniLetoviData.Services.Implementation;

namespace JeftiniLetoviUI
{
    public static class RegisterServices
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddMemoryCache();
            builder.Services.AddTransient<IFlightOffersService, FlightOffersService>();
            builder.Services.AddTransient<IFlightOffersAPICall, FlightOffersAPICall>();
            builder.Services.AddTransient<IAuthenticationTokenAPICall, AuthenticationTokenAPICall>();
            builder.Services.AddTransient<ILoggerClass, LoggerClass>();
        }
    }
}
