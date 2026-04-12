using Domain.Entities.Common;
using Domain.Utilities.CustomException;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities.Employee
{
    internal sealed class Employee : BaseEntity
    {
        public required string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public required string LastName { get; set; }

        public Guid AddressId { get; set; }

        public Guid CompanyId { get; set; }

        public Address? Address { get; set; }

        public Company.Company? Company { get; set; }

        private Employee() { }

        [SetsRequiredMembers]
        public Employee(string firstName, string? middleName, string lastName, Guid addressId, Guid companyId, string createdBy, bool isActive)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            SetAddressId(addressId);
            SetCompanyId(companyId);
            CreatedBy = createdBy;
            IsActive = isActive;
        }

        private void SetAddressId(Guid addressId)
        {
            if (addressId == Guid.Empty)
            {
                throw new DomainException("AddressId cannot be empty.");
            }
            AddressId = addressId;
        }

        private void SetCompanyId(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new DomainException("CompanyId cannot be empty.");
            }
            CompanyId = companyId;
        }
    }
}
