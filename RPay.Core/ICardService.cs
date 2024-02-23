using RPay.Dtos;

namespace RPay.Core
{
    public interface ICardService
    {
        Task<ApiResponse<Guid>> CreateCard(CreateCard card);

        Task<ApiResponse<decimal>> GetCardBalance(Guid cardId);

        Task<ApiResponse<decimal>> PayCard(Guid cardId, decimal amount);

        Task<ApiResponse<decimal>> ProcessTransaction(Guid cardId, decimal amount);
    }
}