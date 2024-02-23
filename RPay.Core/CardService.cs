using RPay.Dtos;
using RPay.Infrastructure.Database.Repositories;

namespace RPay.Core
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IFeeRepository _feeRepository;
        public CardService(ICardRepository cardRepository , IFeeRepository feeRepository)
        {
            _cardRepository = cardRepository;
            _feeRepository = feeRepository;
        }

        public async Task<ApiResponse<decimal>> GetCardBalance(Guid cardId)
        {
            var card = await _cardRepository.GetCard(cardId);

            if(card == null)
            {
                return new ApiResponse<decimal>("Card not found");
            }
            return new ApiResponse<decimal>(card.Balance);
        }

        public async Task<ApiResponse<decimal>> ProcessTransaction(Guid cardId, decimal amount)
        {
            var card = await _cardRepository.GetCard(cardId);
            if (card == null)
            {
                return new ApiResponse<decimal>("Card not found");
            }

            card.Balance = card.Balance + amount;

            await _cardRepository.UpdateCard(card);

            return new ApiResponse<decimal>(card.Balance);
        }

        public async Task<ApiResponse<decimal>> PayCard(Guid cardId, decimal amount)
        {
            var paymentFee = await _feeRepository.GetLatestFee();
            var card = await _cardRepository.GetCard(cardId);
            if (card == null)
            {
                return new ApiResponse<decimal>("Card not found");
            }

            card.Balance = card.Balance - amount;
            card.Payments.Add(new Payment
            {
                CardId = cardId,
                PaymentAmount = amount,
                PaymentFee = paymentFee ?? 0
            });
            await _cardRepository.UpdateCard(card);
         
            return new ApiResponse<decimal>(card.Balance);
        }

        public async Task<ApiResponse<Guid>> CreateCard(CreateCard createCard)
        {
            if(createCard.CardNumber.Length != 15)
            {
                return new ApiResponse<Guid>("Card number should have 15 digits");
            }

            var card = createCard.ToCard(new Guid());
            //card.Balance = 1000;
            await _cardRepository.AddCard(card);

            return new ApiResponse<Guid>(card.Id);
        }
    }
}