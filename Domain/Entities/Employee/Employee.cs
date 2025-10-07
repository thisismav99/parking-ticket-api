using Domain.Entities.Common;

namespace Domain.Entities.Employee
{
    internal sealed class Employee : BaseEntity
    {
        public required string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public required string LastName { get; set; }

        public Guid AddressId { get; set; }

        public Guid CompanyId { get; set; }

        public required Address Address { get; set; }

        public required Company.Company Company { get; set; }
    }
}
