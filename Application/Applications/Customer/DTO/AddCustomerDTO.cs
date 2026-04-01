using Application.DTO;

namespace Application.Applications.Customer.DTO
{
    public class AddCustomerDTO : BaseDTO
    {
        public required string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public required string LastName { get; set; }

        public string? ContactNo { get; set; }

        public string? Email { get; set; }

        public Guid AddressId { get; set; }
    }
}
