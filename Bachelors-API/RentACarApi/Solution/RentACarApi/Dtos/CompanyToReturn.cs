using RentACarApi.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentACarApi.Dtos
{
    public class CompanyToReturn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PromoDescription { get; set; }
        public double AverageGrade { get; set; }
        public double WeekRentalDiscount { get; set; }
        public double MonthRentalDiscount { get; set; }
        public virtual Branch HeadOffice { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public string Photo { get; set; }
        public string Admin { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
