using Application.Applications.Customer.DTO;
using Infrastructure.Interfaces.Customer;
using Moq;
using ParkingTicketingAPI.Test.UnitTests.Fixtures;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Customer.Fixture
{
    public class CustomerFixture : BaseFixture
    {
        private readonly Mock<ICustomerService> _mockCustomerService;

        public CustomerFixture()
        {
            _mockCustomerService = new Mock<ICustomerService>(MockBehavior.Strict);
        }

        internal Mock<ICustomerService> MockCustomerService() => _mockCustomerService;

        public Guid GetCustomerGuid() => guid;

        #region AddCustomerCommandHandler
        public AddCustomerDTO ValidAddCustomerDTO()
        {
            return new AddCustomerDTO()
            {
                FirstName = "Jane",
                MiddleName = "M",
                LastName = "Doe",
                ContactNo = "+639171234567",
                Email = "jane.doe@test.com",
                AddressId = guid,
                CreatedBy = "User 1",
                IsActive = true
            };
        }

        public void SetupAddCustomer()
        {
            _mockCustomerService.Setup(c => c.AddCustomer(It.IsAny<Domain.Entities.Customer.Customer>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(guid);
        }

        public void VerifyAddCustomer()
        {
            _mockCustomerService.Verify(c => c.AddCustomer(It.IsAny<Domain.Entities.Customer.Customer>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion

        #region GetCustomerByIdQueryHandler
        internal Domain.Entities.Customer.Customer ValidGetCustomerById()
        {
            return new Domain.Entities.Customer.Customer("Jane",
                "M",
                "Doe",
                "+639171234567",
                "jane.doe@test.com",
                guid,
                "User 1",
                true);
        }

        public void SetupValidGetByIdCustomer()
        {
            _mockCustomerService.Setup(c => c.GetCustomerById(It.Is<Guid>(id => id == guid), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ValidGetCustomerById());
        }

        public void SetupInvalidGetByIdCustomer()
        {
            _mockCustomerService.Setup(c => c.GetCustomerById(It.Is<Guid>(id => id == invalidGuid), It.IsAny<CancellationToken>()))
               .ReturnsAsync((Domain.Entities.Customer.Customer?)null);
        }

        public void VerifyValidGetByIdCustomer()
        {
            _mockCustomerService.Verify(c => c.GetCustomerById(It.Is<Guid>(id => id == guid), It.IsAny<CancellationToken>()), Times.Once);
        }

        public void VerifyInvalidGetByIdCustomer()
        {
            _mockCustomerService.Verify(c => c.GetCustomerById(It.Is<Guid>(id => id == invalidGuid), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion

        #region GetCustomersQueryHandler
        internal List<Domain.Entities.Customer.Customer> ValidGetCustomers()
        {
            return new List<Domain.Entities.Customer.Customer>()
            {
                new Domain.Entities.Customer.Customer("Jane",
                "M",
                "Doe",
                "+639171234567",
                "jane@test.com",
                guid,
                "User 1",
                true),
                new Domain.Entities.Customer.Customer("John",
                null,
                "Smith",
                null,
                "john@test.com",
                guid,
                "User 2",
                true),
                new Domain.Entities.Customer.Customer("Ann",
                "K",
                "Lee",
                null,
                "ann@test.com",
                guid,
                "User 3",
                true)
            };
        }

        public void SetupGetCustomers()
        {
            _mockCustomerService.Setup(c => c.GetCustomers(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ValidGetCustomers());
        }

        public void VerifyGetCustomers()
        {
            _mockCustomerService.Verify(c => c.GetCustomers(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion
    }
}
