using Application.Applications.User.DTO;
using Application.Applications.User.Query;
using Mapster;
using ParkingTicketingAPI.Test.UnitTests.Applications.User.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.User
{
    public sealed class GetUsersQueryHandlerTest : IClassFixture<UserFixture>
    {
        private readonly UserFixture _userFixture;

        public GetUsersQueryHandlerTest(UserFixture userFixture)
        {
            _userFixture = userFixture;
        }

        [Fact(DisplayName = "User list should be returned when data exists")]
        public async Task GetUsersQueryHandlerReturnsList()
        {
            // Arrange
            _userFixture.SetupGetUsers();

            // Act
            var sut = new GetUsersQueryHandler(_userFixture.MockUserService().Object);

            var result = await sut.Handle(new GetUsersQuery(1, 10), CancellationToken.None);
            result.Adapt<List<ResponseUserDTO>>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ResponseUserDTO>>(result);
            _userFixture.VerifyGetUsers();
        }
    }
}
