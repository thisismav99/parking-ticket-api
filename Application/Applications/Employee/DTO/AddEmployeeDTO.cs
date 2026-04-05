using Application.DTO;

namespace Application.Applications.Employee.DTO
{
    public class AddEmployeeDTO : BaseDTO
    {
        public required string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public required string LastName { get; set; }

        public Guid AddressId { get; set; }

        public Guid CompanyId { get; set; }
    }
}
