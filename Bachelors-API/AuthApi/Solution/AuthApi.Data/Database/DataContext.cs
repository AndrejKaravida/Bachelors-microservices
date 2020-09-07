using AuthApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
    }

}