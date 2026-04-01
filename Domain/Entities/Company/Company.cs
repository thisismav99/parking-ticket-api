using Domain.Entities.Common;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities.Company
{
    internal sealed class Company : BaseEntity
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        private Company() { }

        [SetsRequiredMembers]
        public Company(string name, string? description, string createdBy, bool isActive)
        {
            Name = name;
            Description = description;
            CreatedBy = createdBy;
            IsActive = isActive;
        }
    }
}
