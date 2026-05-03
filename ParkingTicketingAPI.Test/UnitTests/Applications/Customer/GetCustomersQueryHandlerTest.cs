using Application.Applications.Customer.DTO;
using Application.Applications.Customer.Query;
using Mapster;
using ParkingTicketingAPI.Test.UnitTests.Applications.Customer.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Customer
{
    public sealed class GetCustomersQueryHandlerTest : IClassFixture<CustomerFixture>
    {
        private readonly CustomerFixture _customerFixture;

        public GetCustomersQueryHandlerTest(CustomerFixture customerFixture)
        {
            _customerFixture = customerFixture;
        }

        [Fact(DisplayName = "Customer list should be returned when data exists")]
        public async Task GetCustomersQueryHandlerReturnsList()
        {
            // Arrange
            _customerFixture.SetupGetCustomers();

            // Act
            var sut = new GetCustomersQueryHandler(_customerFixture.MockCustomerService().Object);

            var result = await sut.Handle(new GetCustomersQuery(1, 10), CancellationToken.None);
            result.Adapt<List<ResponseCustomerDTO>>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ResponseCustomerDTO>>(result);
            _customerFixture.VerifyGetCustomers();
        }
    }
}
