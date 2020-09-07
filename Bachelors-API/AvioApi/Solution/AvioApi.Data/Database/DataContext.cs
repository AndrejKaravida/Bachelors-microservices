using AvioApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AvioApi.Data.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet<FlightReservation> FlightReservations { get; set; }
        public DbSet<CompanyRating> AvioCompanyRatings { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<AirCompany> AirCompanies { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Branch> AirCompanyBranches { get; set; }
    }

}