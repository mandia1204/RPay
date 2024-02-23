using RPay.Dtos;

namespace RPay.Infrastructure.Database.Repositories
{
    public interface ICardRepository
    {
        Task<int> AddCard(Card card);

        Task<int> UpdateCard(Card card);

        Task<Card?> GetCard(Guid cardId);

        Task<Card?> GetCardByNumber(string cardNumber);
    }
}
