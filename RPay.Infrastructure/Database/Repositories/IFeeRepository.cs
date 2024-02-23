using RPay.Dtos;

namespace RPay.Infrastructure.Database.Repositories
{
    public interface IFeeRepository
    {
        Task<decimal?> GetLatestFee();

        Task<int> AddFee(decimal amount);
    }
}
