using Application.DTO;
using System.ComponentModel.DataAnnotations;

namespace Application.Applications.Common.DTO
{
    public class AddAddressDTO : BaseDTO
    {
        public int? LotNo { get; set; }

        [Required, MaxLength(50)]
        public required string Street { get; set; }

        [Required, MaxLength(50)]
        public required string Barangay { get; set; }

        [Required, MaxLength(50)]
        public required string Municipality { get; set; }

        [Required, MaxLength(50)]
        public required string Region { get; set; }

        [Required, MaxLength(100)]
        public required string Country { get; set; }
    }
}
