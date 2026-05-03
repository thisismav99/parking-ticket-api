using Application.Applications.Common.Command;
using ParkingTicketingAPI.Test.UnitTests.Applications.Common.Address.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Common.Address
{
    public sealed class AddAddressCommandHandlerTest : IClassFixture<AddressFixture>
    {
        private readonly AddressFixture _addressFixture;

        public AddAddressCommandHandlerTest(AddressFixture addressFixture)
        {
            _addressFixture = addressFixture;
        }

        [Fact(DisplayName =  "Address Id should be returned when adding a valid address")]
        public async Task AddAddressCommandHandlerSuccess()
        {
            // Arrange
            _addressFixture.SetupAddAddress();

            // Act
            var sut = new AddAddressCommandHandler(_addressFixture.MockAddressService().Object);

            var result = await sut.Handle(new AddAddressCommand(_addressFixture.ValidAddAddressDTO()), CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(_addressFixture.GetAddressGuid(), result.Value);
            Assert.IsType<Guid>(result.Value);
            _addressFixture.VerifyAddAddress();
        }
    }
}
