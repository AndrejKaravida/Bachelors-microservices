namespace RentACarApi.Data
{
    public class DiscountedVehiclesParams
    {
        public string pickupLocation { get; set; } = "";
        public string startingDate { get; set; } = "";
        public int numberOfDays { get; set; } = 0;
    }
}
