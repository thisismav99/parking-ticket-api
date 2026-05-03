using Application.Applications.Customer.Command;
using ParkingTicketingAPI.Test.UnitTests.Applications.Customer.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Customer
{
    public sealed class AddCustomerCommandHandlerTest : IClassFixture<CustomerFixture>
    {
        private readonly CustomerFixture _customerFixture;

        public AddCustomerCommandHandlerTest(CustomerFixture customerFixture)
        {
            _customerFixture = customerFixture;
        }

        [Fact(DisplayName = "Customer Id should be returned when adding a valid customer")]
        public async Task AddCustomerCommandHandlerSuccess()
        {
            // Arrange
            _customerFixture.SetupAddCustomer();

            // Act
            var sut = new AddCustomerCommandHandler(_customerFixture.MockCustomerService().Object);

            var result = await sut.Handle(new AddCustomerCommand(_customerFixture.ValidAddCustomerDTO()), CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(_customerFixture.GetCustomerGuid(), result.Value);
            Assert.IsType<Guid>(result.Value);
            _customerFixture.VerifyAddCustomer();
        }
    }
}
