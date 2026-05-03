using Application.Applications.Parking.Command;
using ParkingTicketingAPI.Test.UnitTests.Applications.Parking.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Parking
{
    public sealed class AddParkingCommandHandlerTest : IClassFixture<ParkingFixture>
    {
        private readonly ParkingFixture _parkingFixture;

        public AddParkingCommandHandlerTest(ParkingFixture parkingFixture)
        {
            _parkingFixture = parkingFixture;
        }

        [Fact(DisplayName = "Parking Id should be returned when adding a valid parking")]
        public async Task AddParkingCommandHandlerSuccess()
        {
            // Arrange
            _parkingFixture.SetupAddParking();

            // Act
            var sut = new AddParkingCommandHandler(_parkingFixture.MockParkingService().Object);

            var result = await sut.Handle(new AddParkingCommand(_parkingFixture.ValidAddParkingDTO()), CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(_parkingFixture.GetParkingGuid(), result.Value);
            Assert.IsType<Guid>(result.Value);
            _parkingFixture.VerifyAddParking();
        }
    }
}
