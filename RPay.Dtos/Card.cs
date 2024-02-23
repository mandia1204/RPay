namespace RPay.Dtos
{
    public class Card
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardNumber { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public decimal Balance { get; set; } = 0;
        public decimal CreditLimit { get; set; }
        public ICollection<Payment> Payments { get; } = new List<Payment>();
    }

    public class CreateCard
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardNumber { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public decimal CreditLimit { get; set; }

        public Card ToCard(Guid Id)
        {
            return new Card {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                CardNumber = CardNumber,
                ExpirationMonth = ExpirationMonth,
                ExpirationYear = ExpirationYear,
                CreditLimit = CreditLimit
            };
        }
    }
}