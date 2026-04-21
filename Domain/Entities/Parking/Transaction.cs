using Domain.Entities.Common;
using Domain.Utilities.CustomException;
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

        public Guid ParkingId { get; set; }

        public Parking? Parking { get; set; }

        private Transaction() { }

        [SetsRequiredMembers]
        public Transaction(decimal amountPaid, 
            bool isCard, 
            bool isCash, 
            Guid parkingId,
            string createdBy, 
            bool isActive)
        {
            SetAmountPaid(amountPaid);
            IsCard = isCard;
            IsCash = isCash;
            SetParkingId(parkingId);
            CreatedBy = createdBy;
            IsActive = isActive;
        }

        private void SetAmountPaid(decimal amountPaid)
        {
            if (amountPaid <= 0)
            {
                throw new DomainException("AmountPaid cannot be zero or negative.");
            }
            AmountPaid = amountPaid;
        }

        private void SetParkingId(Guid parkingId)
        {
            if (parkingId == Guid.Empty)
            {
                throw new DomainException("ParkingId cannot be empty.");
            }
            ParkingId = parkingId;
        }
    }
}
