using Application.Applications.Common.Command;
using ParkingTicketingAPI.Test.UnitTests.Applications.Common.Address.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Common.Address
{
    public sealed class AddAddressCommandHandlerTest : IClassFixture<AddressFixture>
    {
        private readonly AddressFixture _fixture;

        public AddAddressCommandHandlerTest(AddressFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName =  "Address Id should be returned when adding a valid address")]
        public async Task AddAddressCommandHandlerSuccess()
        {
            // Arrange
            _fixture.SetupAddAddress();

            // Act
            var sut = new AddAddressCommandHandler(_fixture.MockAddressService().Object);

            var result = await sut.Handle(new AddAddressCommand(_fixture.ValidAddressDTO()), CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(_fixture.GetAddressGuid(), result.Value);
            _fixture.VerifyAddAddress();
        }
    }
}
