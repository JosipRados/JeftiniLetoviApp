namespace JeftiniLetoviUI.Models
{
    public class FlightOffersModel
    {
        public string? DepartureAirport { get; set; }
        public string? ArrivalAirport { get; set; }
        public DateOnly DepartureDate { get; set; }
        public DateOnly ReturnDate { get; set; }
        public int DepartureConnections { get; set; }
        public int ReturnConnections { get; set; }
        public int NumberOfSeats { get; set; }
        public string? Currency { get; set; }
        public float Price { get; set; }
    }
}
