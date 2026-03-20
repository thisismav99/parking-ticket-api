using Domain.Entities.Common;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities.Parking
{
    internal sealed class Transaction : BaseEntity
    {
        public decimal AmountToPay { get; set; }

        public decimal AmountPaid { get; set; }

        public decimal AmountChange { get; set; }

        public bool IsCard { get; set; }

        public bool IsCash { get; set; }

        private Transaction() { }

        [SetsRequiredMembers]
        public Transaction(decimal amountToPay, decimal amountPaid, bool isCard, bool isCash, string createdBy, bool isActive)
        {
            AmountToPay = amountToPay;
            AmountPaid = amountPaid;
            IsCard = isCard;
            IsCash = isCash;
            CreatedBy = createdBy;
            IsActive = isActive;
        }
    }
}
