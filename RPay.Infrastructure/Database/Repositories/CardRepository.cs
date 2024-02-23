using Microsoft.EntityFrameworkCore;
using RPay.Dtos;

namespace RPay.Infrastructure.Database.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly IApiDbContext _dbContext;
        public CardRepository(IApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> AddCard(Card card)
        {
            _dbContext.Cards.Add(card);
            return _dbContext.SaveChangesAsync();
        }

        public Task<Card?> GetCard(Guid cardId)
        {
            return _dbContext.Cards.FirstOrDefaultAsync(c => c.Id == cardId);
        }

        public Task<Card?> GetCardByNumber(string cardNumber)
        {
            return _dbContext.Cards.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
        }

        public Task<int> UpdateCard(Card card)
        {
            _dbContext.Cards.Update(card);
            return _dbContext.SaveChangesAsync();
        }
    }
}
