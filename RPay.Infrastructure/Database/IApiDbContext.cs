using Microsoft.EntityFrameworkCore;
using RPay.Dtos;

namespace RPay.Infrastructure.Database
{
    public interface IApiDbContext
    {
        DbSet<Card> Cards { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<Fee> Fees { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
