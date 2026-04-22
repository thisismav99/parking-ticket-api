using Domain.Entities.Common;
using Domain.Utilities.Exceptions;
using System.Diagnostics.CodeAnalysis;

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

        public Address? Address { get; set; }

        private Customer() { }

        [SetsRequiredMembers]
        public Customer(string firstName, 
            string? middleName, 
            string lastName, 
            string? contactNo, 
            string? email, 
            Guid addressId,
            string createdBy, 
            bool isActive)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            ContactNo = contactNo;
            SetEmail(email);
            SetAddressId(addressId);
            CreatedBy = createdBy;
            IsActive = isActive;
        }

        private void SetEmail(string? email)
        {
            if (!string.IsNullOrEmpty(email) && !email.Contains('@'))
            {
                throw new DomainException("Invalid email format.");
            }

            Email = email;
        } 

        private void SetAddressId(Guid addressId)
        {
            if (addressId == Guid.Empty)
            {
                throw new DomainException("AddressId cannot be empty.");
            }

            AddressId = addressId;
        }
    }
}
