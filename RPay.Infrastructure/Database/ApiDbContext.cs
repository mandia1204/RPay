using Microsoft.EntityFrameworkCore;
using RPay.Dtos;

namespace RPay.Infrastructure.Database
{
    public class ApiDbContext : DbContext, IApiDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "RPayDb");
        }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Fee> Fees { get; set; }

    }
}
