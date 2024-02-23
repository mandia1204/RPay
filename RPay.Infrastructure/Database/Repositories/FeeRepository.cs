using Microsoft.EntityFrameworkCore;
using RPay.Dtos;

namespace RPay.Infrastructure.Database.Repositories
{
    public class FeeRepository: IFeeRepository
    {
        private readonly IApiDbContext _dbContext;
        public FeeRepository(IApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<decimal?> GetLatestFee()
        {
            var fee = await _dbContext.Fees.LastOrDefaultAsync();

            return fee?.Value;
        }

        public Task<int> AddFee(decimal amount)
        {
            _dbContext.Fees.Add(new Fee { Date = DateTime.Now, Value = amount});
            return _dbContext.SaveChangesAsync();
        }
    }
}
