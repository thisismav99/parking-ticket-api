namespace Domain.Entities.Company
{
    internal sealed class Company : BaseEntity
    {
        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
