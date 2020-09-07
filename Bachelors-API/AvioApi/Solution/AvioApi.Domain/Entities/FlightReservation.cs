using System;

namespace AvioApi.Domain.Entities
{
    public class FlightReservation
    {
        public int Id { get; set; }
        public string UserAuthId { get; set; }
        public int CompanyId { get; set; }
        public virtual Flight Flight { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhoto { get; set; }
        public string DepartureDestination { get; set; }
        public string ArrivalDestination { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public double Price { get; set; }
        public double TravelLength { get; set; }
        public string Status { get; set; }
    }
}