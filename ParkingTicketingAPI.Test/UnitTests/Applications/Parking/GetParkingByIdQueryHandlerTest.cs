using Application.Applications.Parking.DTO;
using Application.Applications.Parking.Query;
using Application.Utilities.Helpers;
using Mapster;
using ParkingTicketingAPI.Test.UnitTests.Applications.Parking.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Parking
{
    public sealed class GetParkingByIdQueryHandlerTest : IClassFixture<ParkingFixture>
    {
        private readonly ParkingFixture _parkingFixture;

        public GetParkingByIdQueryHandlerTest(ParkingFixture parkingFixture)
        {
            _parkingFixture = parkingFixture;
        }

        [Theory(DisplayName = "Parking should be returned when a valid parking id is entered and not found when invalid")]
        [InlineData("3f9b2a6c-8d4e-4f71-9c2a-5e7b1a93d6f8")]
        [InlineData("4f9b2a6c-8d4e-4f71-9c2a-5e7b1a93d6f9")]
        public async Task GetParkingByIdQueryHandlerReturns(string guid)
        {
            // Arrange
            Guid parkingId = Guid.Parse(guid);
            _parkingFixture.SetupValidGetByIdParking();
            _parkingFixture.SetupInvalidGetByIdParking();

            // Act
            var sut = new GetParkingByIdQueryHandler(_parkingFixture.MockParkingService().Object);

            var result = await sut.Handle(new GetParkingByIdQuery(parkingId), CancellationToken.None);
            result.Adapt<ResponseParkingDTO>();

            // Assert
            if (parkingId == _parkingFixture.GetParkingGuid())
            {
                Assert.True(result.IsSuccess);
                Assert.NotNull(result.Value);
                Assert.IsType<ResponseParkingDTO>(result.Value);
                _parkingFixture.VerifyValidGetByIdParking();
            }
            else
            {
                Assert.True(result.IsFailure);
                Assert.Equal(GetError.NotFound(parkingId), result.Error);
                Assert.IsType<string>(result.Error);
                _parkingFixture.VerifyInvalidGetByIdParking();
            }
        }
    }
}
