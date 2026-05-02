using Application.Applications.Common.DTO;
using Infrastructure.Interfaces.Common;
using Moq;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Common.Address.Fixture
{
    public class AddressFixture
    {
        private readonly Mock<IAddressService> _mockAddressService;
        private Guid addressGuid = Guid.Parse("3f9b2a6c-8d4e-4f71-9c2a-5e7b1a93d6f8");

        public AddressFixture()
        {
            _mockAddressService = new Mock<IAddressService>(MockBehavior.Strict);
        }

        internal Mock<IAddressService> MockAddressService() => _mockAddressService;

        public Guid GetAddressGuid() => addressGuid;

        public AddAddressDTO ValidAddressDTO()
        {
            return new AddAddressDTO()
            {
                LotNo = 123,
                Street = "Main St",
                Barangay = "Barangay 1",
                Municipality = "Municipality 1",
                Region = "Region 1",
                Country = "Country 1",
                CreatedBy = "User 1",
                IsActive = true
            };
        }

        public void SetupAddAddress()
        {
            _mockAddressService.Setup(a => a.AddAddress(It.IsAny<Domain.Entities.Common.Address>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(addressGuid);
        }

        public void VerifyAddAddress()
        {
            _mockAddressService.Verify(a => a.AddAddress(It.IsAny<Domain.Entities.Common.Address>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
