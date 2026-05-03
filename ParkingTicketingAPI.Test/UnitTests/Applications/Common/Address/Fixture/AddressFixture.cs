using Application.Applications.Common.DTO;
using Infrastructure.Interfaces.Common;
using Moq;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Common.Address.Fixture
{
    public class AddressFixture
    {
        private readonly Mock<IAddressService> _mockAddressService;
        private Guid addressGuid = Guid.Parse("3f9b2a6c-8d4e-4f71-9c2a-5e7b1a93d6f8");
        private Guid invalidAddressGuid = Guid.Parse("4f9b2a6c-8d4e-4f71-9c2a-5e7b1a93d6f9");

        public AddressFixture()
        {
            _mockAddressService = new Mock<IAddressService>(MockBehavior.Strict);
        }

        internal Mock<IAddressService> MockAddressService() => _mockAddressService;

        public Guid GetAddressGuid() => addressGuid;

        #region AddAddressCommandHandler
        public AddAddressDTO ValidAddAddressDTO()
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
        #endregion

        #region GetAddressByIdQueryHandler
        internal Domain.Entities.Common.Address ValidGetAddressById()
        {
            return new Domain.Entities.Common.Address(123,
                "Street 1",
                "Barangay 1",
                "Municipality 1",
                "Region 1",
                "Country 1",
                "User 1",
                true);
        }

        public void SetupValidGetByIdAddress()
        {
             _mockAddressService.Setup(a => a.GetAddressById(It.Is<Guid>(id => id == addressGuid), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ValidGetAddressById());
        }

        public void SetupInvalidGetByIdAddress()
        {
            _mockAddressService.Setup(a => a.GetAddressById(It.Is<Guid>(id => id == invalidAddressGuid), It.IsAny<CancellationToken>()))
               .ReturnsAsync((Domain.Entities.Common.Address?)null);
        }

        public void VerifyValidGetByIdAddress()
        {
            _mockAddressService.Verify(a => a.GetAddressById(It.Is<Guid>(id => id == addressGuid), It.IsAny<CancellationToken>()), Times.Once);
        }

        public void VerifyInvalidGetByIdAddress()
        {
            _mockAddressService.Verify(a => a.GetAddressById(It.Is<Guid>(id => id == invalidAddressGuid), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion

        #region GetAddressesQueryHandler
        internal List<Domain.Entities.Common.Address> ValidGetAddresses()
        {
            return new List<Domain.Entities.Common.Address>()
            {
                new Domain.Entities.Common.Address(123,
                "Street 1",
                "Barangay 1",
                "Municipality 1",
                "Region 1",
                "Country 1",
                "User 1",
                true),
                new Domain.Entities.Common.Address(145,
                "Street 2",
                "Barangay 2",
                "Municipality 2",
                "Region 2",
                "Country 2",
                "User 2",
                true),
                new Domain.Entities.Common.Address(128,
                "Street 3",
                "Barangay 3",
                "Municipality 3",
                "Region 3",
                "Country 3",
                "User 3",
                true)
            };
        }

        public void SetupGetAddresses()
        {
            _mockAddressService.Setup(a => a.GetAddresses(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ValidGetAddresses());
        }

        public void VerifyGetAddresses()
        {
            _mockAddressService.Verify(a => a.GetAddresses(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion
    }
}
