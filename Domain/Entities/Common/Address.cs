using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities.Common
{
    internal sealed class Address : BaseEntity
    {
        public int? LotNo { get; set; }

        public required string Street { get; set; }

        public required string Barangay { get; set; }

        public required string Municipality { get; set; }

        public required string Region { get; set; }

        public required string Country { get; set; }

        private Address() { }

        [SetsRequiredMembers]
        public Address(int? lotNo, 
            string street, 
            string barangay, 
            string municipality, 
            string region, 
            string country, 
            string createdBy, 
            bool isActive)
        {
            LotNo = lotNo;
            Street = street;
            Barangay = barangay;
            Municipality = municipality;
            Region = region;
            Country = country;
            CreatedBy = createdBy;
            IsActive = isActive;
        }
    }
}
