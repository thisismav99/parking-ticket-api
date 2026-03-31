namespace Application.Customer.DTO
{
    public class AddCustomerDTO
    {
        public required string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public required string LastName { get; set; }

        public string? ContactNo { get; set; }

        public string? Email { get; set; }

        public int? LotNo { get; set; }

        public required string Street { get; set; }

        public required string Barangay { get; set; }

        public required string Municipality { get; set; }

        public required string Region { get; set; }

        public required string Country { get; set; }

        public required string CreatedBy { get; set; }

        public bool IsActive { get; set; }
    }
}
