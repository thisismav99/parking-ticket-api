using Application.Applications.Vehicle.DTO;
using Application.Applications.Vehicle.Query;
using Mapster;
using ParkingTicketingAPI.Test.UnitTests.Applications.Vehicle.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Vehicle
{
    public sealed class GetVehiclesQueryHandlerTest : IClassFixture<VehicleFixture>
    {
        private readonly VehicleFixture _vehicleFixture;

        public GetVehiclesQueryHandlerTest(VehicleFixture vehicleFixture)
        {
            _vehicleFixture = vehicleFixture;
        }

        [Fact(DisplayName = "Vehicle list should be returned when data exists")]
        public async Task GetVehiclesQueryHandlerReturnsList()
        {
            // Arrange
            _vehicleFixture.SetupGetVehicles();

            // Act
            var sut = new GetVehiclesQueryHandler(_vehicleFixture.MockVehicleService().Object);

            var result = await sut.Handle(new GetVehiclesQuery(1, 10), CancellationToken.None);
            result.Adapt<List<ResponseVehicleDTO>>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ResponseVehicleDTO>>(result);
            _vehicleFixture.VerifyGetVehicles();
        }
    }
}
