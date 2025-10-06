namespace Domain.Entities.Customer
{
    internal sealed class Customer : BaseEntity
    {
        public required string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public required string LastName { get; set; }

        public string? ContactNo { get; set; }

        public string? Email { get; set; }

        public Guid AddressId { get; set; }

        public required Address Address { get; set; }
    }
}
