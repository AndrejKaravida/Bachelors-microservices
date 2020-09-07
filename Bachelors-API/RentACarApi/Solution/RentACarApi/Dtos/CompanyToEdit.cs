using RentACarApi.Domain;
using System.ComponentModel.DataAnnotations;

namespace RentACarApi.Dtos
{
    public class CompanyToEdit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PromoDescription { get; set; }
        public double WeekRentalDiscount { get; set; }
        public double MonthRentalDiscount { get; set; }
        public string Admin { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
