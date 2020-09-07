namespace RentACarApi.Domain
{
    public class ReservationStatsToReturn
    {
        public int ReservationsToday { get; set; }
        public int ReservationsThisWeek { get; set; }
        public int ReservationsThisMonth { get; set; }
    }
}
