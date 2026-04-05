namespace Application.Applications.Employee.DTO
{
    internal class ResponseEmployeeDTO
    {
        public required string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public required string LastName { get; set; }

        public Guid AddressId { get; set; }

        public Guid CompanyId { get; set; }
    }
}
