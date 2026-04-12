using Application.DTO;
using System.ComponentModel.DataAnnotations;

namespace Application.Applications.Parking.DTO
{
    public class AddTransactionDTO : BaseDTO
    {
        [Required]
        public decimal AmountToPay { get; set; }

        [Required]
        public decimal AmountPaid { get; set; }

        [Required, Range(0,1)]
        public bool IsCard { get; set; }

        [Required, Range(0, 1)]
        public bool IsCash { get; set; }
    }
}
