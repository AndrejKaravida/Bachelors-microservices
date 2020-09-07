namespace AvioApi.Dtos
{
    public class RateFlightData
    {
        public string UserId { get; set; }
        public int ReservationId { get; set; }
        public int CompanyId { get; set; }
        public int FlightId { get; set; }
        public int CompanyRating { get; set; }
        public int FlightRating { get; set; }
    }
}
