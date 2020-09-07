namespace RentACarApi.Dtos
{
    public class CarReservtion
    {
        public int VehicleId { get; set; }
        public int CompanyId { get; set; }
        public string Startdate { get; set; }
        public string Enddate { get; set; }
        public string AuthId { get; set; }
        public string Companyname { get; set; }
        public string StartingLocation { get; set; }
        public string ReturningLocation { get; set; }
        public int Totaldays { get; set; }
        public double Totalprice { get; set; }
    }
}
