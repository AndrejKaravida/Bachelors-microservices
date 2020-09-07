namespace RentACarApi.Dtos
{
    public class RateData
    {
        public string UserId { get; set; }
        public int ReservationId { get; set; }
        public int CompanyId { get; set; }
        public int VehicleId { get; set; }
        public int CompanyRating { get; set; }
        public int VehicleRating { get; set; }
    }
}
