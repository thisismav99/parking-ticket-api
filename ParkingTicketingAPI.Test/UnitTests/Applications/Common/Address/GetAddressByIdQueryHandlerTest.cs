using Application.Applications.Common.DTO;
using Application.Applications.Common.Query;
using Application.Utilities.Helpers;
using Mapster;
using ParkingTicketingAPI.Test.UnitTests.Applications.Common.Address.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Common.Address
{
    public sealed class GetAddressByIdQueryHandlerTest : IClassFixture<AddressFixture>
    {
        private readonly AddressFixture _addressFixture;

        public GetAddressByIdQueryHandlerTest(AddressFixture addressFixture)
        {
            _addressFixture = addressFixture;
        }

        [Theory(DisplayName = "Address should be returned when a valid address id is entered and not found when invalid")]
        [InlineData("3f9b2a6c-8d4e-4f71-9c2a-5e7b1a93d6f8")]
        [InlineData("4f9b2a6c-8d4e-4f71-9c2a-5e7b1a93d6f9")]
        public async Task GetAddressByIdQueryHandlerReturns(string guid)
        {
            // Arrange
            Guid addressId = Guid.Parse(guid);
            _addressFixture.SetupValidGetByIdAddress();
            _addressFixture.SetupInvalidGetByIdAddress();

            // Act
            var sut = new GetAddressByIdQueryHandler(_addressFixture.MockAddressService().Object);

            var result = await sut.Handle(new GetAddressByIdQuery(addressId), CancellationToken.None);
            result.Adapt<ResponseAddressDTO>();

            // Assert
            if (addressId == _addressFixture.GetAddressGuid())
            {
                Assert.True(result.IsSuccess);
                Assert.NotNull(result.Value);
                Assert.IsType<ResponseAddressDTO>(result.Value);
                _addressFixture.VerifyValidGetByIdAddress();
            }
            else
            {
                Assert.True(result.IsFailure);
                Assert.Equal(GetError.NotFound(addressId), result.Error);
                Assert.IsType<string>(result.Error);
                _addressFixture.VerifyInvalidGetByIdAddress();
            }
        }
    }
}
