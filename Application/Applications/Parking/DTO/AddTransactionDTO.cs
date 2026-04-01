using Application.DTO;

namespace Application.Applications.Parking.DTO
{
    public class AddTransactionDTO : BaseDTO
    {
        public decimal AmountToPay { get; set; }

        public decimal AmountPaid { get; set; }

        public bool IsCard { get; set; }

        public bool IsCash { get; set; }
    }
}
