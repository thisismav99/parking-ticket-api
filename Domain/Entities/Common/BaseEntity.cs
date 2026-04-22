namespace Domain.Entities.Common
{
    internal abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public required string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? Updated { get; set; }

        public bool IsActive { get; set; }
    }
}
