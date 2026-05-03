using Application.Applications.Customer.DTO;
using Application.Applications.Customer.Query;
using Application.Utilities.Helpers;
using Mapster;
using ParkingTicketingAPI.Test.UnitTests.Applications.Customer.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Customer
{
    public sealed class GetCustomerByIdQueryHandlerTest : IClassFixture<CustomerFixture>
    {
        private readonly CustomerFixture _customerFixture;

        public GetCustomerByIdQueryHandlerTest(CustomerFixture customerFixture)
        {
            _customerFixture = customerFixture;
        }

        [Theory(DisplayName = "Customer should be returned when a valid customer id is entered and not found when invalid")]
        [InlineData("3f9b2a6c-8d4e-4f71-9c2a-5e7b1a93d6f8")]
        [InlineData("4f9b2a6c-8d4e-4f71-9c2a-5e7b1a93d6f9")]
        public async Task GetCustomerByIdQueryHandlerReturns(string guid)
        {
            // Arrange
            Guid customerId = Guid.Parse(guid);
            _customerFixture.SetupValidGetByIdCustomer();
            _customerFixture.SetupInvalidGetByIdCustomer();

            // Act
            var sut = new GetCustomerByIdQueryHandler(_customerFixture.MockCustomerService().Object);

            var result = await sut.Handle(new GetCustomerByIdQuery(customerId), CancellationToken.None);
            result.Adapt<ResponseCustomerDTO>();

            // Assert
            if (customerId == _customerFixture.GetCustomerGuid())
            {
                Assert.True(result.IsSuccess);
                Assert.NotNull(result.Value);
                Assert.IsType<ResponseCustomerDTO>(result.Value);
                _customerFixture.VerifyValidGetByIdCustomer();
            }
            else
            {
                Assert.True(result.IsFailure);
                Assert.Equal(GetError.NotFound(customerId), result.Error);
                Assert.IsType<string>(result.Error);
                _customerFixture.VerifyInvalidGetByIdCustomer();
            }
        }
    }
}
