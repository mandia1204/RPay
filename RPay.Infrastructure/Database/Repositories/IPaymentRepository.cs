using RPay.Dtos;

namespace RPay.Infrastructure.Database.Repositories
{
    public interface IPaymentRepository
    {
        Task<int> AddPayment(Payment payment);

        IAsyncEnumerable<Payment> GetAll();
    }
}
