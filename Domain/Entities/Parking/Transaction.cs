using Domain.Entities.Common;

namespace Domain.Entities.Parking
{
    internal sealed class Transaction : BaseEntity
    {
        public decimal AmountToPay { get; set; }

        public decimal AmountPaid { get; set; }

        public decimal AmountChange { get; set; }

        public bool IsCard { get; set; }

        public bool IsCash { get; set; }
    }
}
