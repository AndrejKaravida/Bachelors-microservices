using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentACarApi.Domain
{
    public class RentACarCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PromoDescription { get; set; }
        public double AverageGrade { get; set; }
        public virtual ICollection<CompanyRating> Ratings { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public double WeekRentalDiscount { get; set; }
        public double MonthRentalDiscount { get; set; }
        public virtual ICollection<Income> Incomes { get; set; }
        public virtual Branch HeadOffice { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public string Photo { get; set; }
        public string Admin { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
