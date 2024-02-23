using System.Reflection.Metadata;

namespace RPay.Dtos
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid CardId { get; set; }
        public decimal PaymentFee { get; set; }
        public decimal PaymentAmount { get; set; }
        public Card Card { get; set; } = null!;
    }
}
