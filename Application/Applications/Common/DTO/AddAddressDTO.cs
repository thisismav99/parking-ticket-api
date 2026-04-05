using Application.DTO;

namespace Application.Applications.Common.DTO
{
    public class AddAddressDTO : BaseDTO
    {
        public int? LotNo { get; set; }

        public required string Street { get; set; }

        public required string Barangay { get; set; }

        public required string Municipality { get; set; }

        public required string Region { get; set; }

        public required string Country { get; set; }
    }
}
