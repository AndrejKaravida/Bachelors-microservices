using AvioApi.Domain.Entities;

namespace AvioApi.Dtos
{
    public class AirCompanyToReturn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PromoDescription { get; set; }
        public double AverageGrade { get; set; }
        public string Photo { get; set; }
        public virtual Branch HeadOffice { get; set; }
        public string Admin { get; set; }
    }
}
