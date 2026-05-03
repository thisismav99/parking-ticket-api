using Application.Applications.Parking.DTO;
using Application.Applications.Parking.Query;
using Mapster;
using ParkingTicketingAPI.Test.UnitTests.Applications.Parking.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Parking
{
    public sealed class GetParkingsQueryHandlerTest : IClassFixture<ParkingFixture>
    {
        private readonly ParkingFixture _parkingFixture;

        public GetParkingsQueryHandlerTest(ParkingFixture parkingFixture)
        {
            _parkingFixture = parkingFixture;
        }

        [Fact(DisplayName = "Parking list should be returned when data exists")]
        public async Task GetParkingsQueryHandlerReturnsList()
        {
            // Arrange
            _parkingFixture.SetupGetParkings();

            // Act
            var sut = new GetParkingsQuer(_parkingFixture.MockParkingService().Object);

            var result = await sut.Handle(new GetParkingsQuery(1, 10), CancellationToken.None);
            result.Adapt<List<ResponseParkingDTO>>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ResponseParkingDTO>>(result);
            _parkingFixture.VerifyGetParkings();
        }
    }
}
