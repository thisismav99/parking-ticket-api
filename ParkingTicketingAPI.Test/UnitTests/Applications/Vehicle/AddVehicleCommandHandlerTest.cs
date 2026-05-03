using Application.Applications.Vehicle.Command;
using ParkingTicketingAPI.Test.UnitTests.Applications.Vehicle.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Vehicle
{
    public sealed class AddVehicleCommandHandlerTest : IClassFixture<VehicleFixture>
    {
        private readonly VehicleFixture _vehicleFixture;

        public AddVehicleCommandHandlerTest(VehicleFixture vehicleFixture)
        {
            _vehicleFixture = vehicleFixture;
        }

        [Fact(DisplayName = "Vehicle Id should be returned when adding a valid vehicle")]
        public async Task AddVehicleCommandHandlerSuccess()
        {
            // Arrange
            _vehicleFixture.SetupAddVehicle();

            // Act
            var sut = new AddVehicleCommandHandler(_vehicleFixture.MockVehicleService().Object);

            var result = await sut.Handle(new AddVehicleCommand(_vehicleFixture.ValidAddVehicleDTO()), CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(_vehicleFixture.GetVehicleGuid(), result.Value);
            Assert.IsType<Guid>(result.Value);
            _vehicleFixture.VerifyAddVehicle();
        }
    }
}
