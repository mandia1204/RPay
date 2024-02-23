using Microsoft.EntityFrameworkCore;
using RPay.Dtos;

namespace RPay.Infrastructure.Database.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IApiDbContext _dbContext;
        public PaymentRepository(IApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> AddPayment(Payment payment)
        {
            _dbContext.Payments.Add(payment);
            return _dbContext.SaveChangesAsync();
        }

        public IAsyncEnumerable<Payment> GetAll()
        {
            return _dbContext.Payments.AsAsyncEnumerable();
        }
    }
}
