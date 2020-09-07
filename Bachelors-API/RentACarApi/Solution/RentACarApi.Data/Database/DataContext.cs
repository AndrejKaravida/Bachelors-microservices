using Microsoft.EntityFrameworkCore;
using RentACarApi.Domain;

namespace RentACarApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<CompanyRating> CompanyRatings { get; set; }
        public DbSet<VehicleRating> VehicleRatings { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<RentACarCompany> RentACarCompanies { get; set; }
        public DbSet<Branch> Branches { get; set; }

    }
}