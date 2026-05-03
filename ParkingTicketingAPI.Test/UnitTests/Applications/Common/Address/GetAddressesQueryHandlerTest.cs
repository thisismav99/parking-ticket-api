using Application.Applications.Common.DTO;
using Application.Applications.Common.Query;
using Mapster;
using ParkingTicketingAPI.Test.UnitTests.Applications.Common.Address.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Common.Address
{
    public sealed class GetAddressesQueryHandlerTest : IClassFixture<AddressFixture>
    {
        private readonly AddressFixture _addressFixture;

        public GetAddressesQueryHandlerTest(AddressFixture addressFixture)
        {
            _addressFixture = addressFixture;
        }

        [Fact(DisplayName = "Address list should be returned when data exists")]
        public async Task GetAddressesQueryHandlerReturnsList()
        {
            // Arrange
            _addressFixture.SetupGetAddresses();

            // Act
            var sut = new GetAddressesQueryHandler(_addressFixture.MockAddressService().Object);

            var result = await sut.Handle(new GetAddressesQuery(1, 10), CancellationToken.None);
            result.Adapt<List<ResponseAddressDTO>>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ResponseAddressDTO>>(result);
            _addressFixture.VerifyGetAddresses();
        }
    }
}
