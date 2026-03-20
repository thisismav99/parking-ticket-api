namespace Application.Parking.DTO
{
    public class AddTransactionDTO
    {
        public decimal AmountToPay { get; set; }

        public decimal AmountPaid { get; set; }

        public bool IsCard { get; set; }

        public bool IsCash { get; set; }

        public required string CreateadBy { get; set; }

        public bool IsActive { get; set; }
    }
}
