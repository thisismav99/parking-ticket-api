using Application.Applications.User.Command;
using ParkingTicketingAPI.Test.UnitTests.Applications.User.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.User
{
    public sealed class AddUserCommandHandlerTest : IClassFixture<UserFixture>
    {
        private readonly UserFixture _userFixture;

        public AddUserCommandHandlerTest(UserFixture userFixture)
        {
            _userFixture = userFixture;
        }

        [Fact(DisplayName = "Identity user id should be returned when adding a valid user")]
        public async Task AddUserCommandHandlerSuccess()
        {
            // Arrange
            _userFixture.SetupAddUser();

            // Act
            var sut = new AddUserCommandHandler(_userFixture.MockUserService().Object);

            var result = await sut.Handle(new AddUserCommand(_userFixture.ValidAddUserDTO()), CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("new-identity-id", result.Value);
            Assert.IsType<string>(result.Value);
            _userFixture.VerifyAddUser();
        }
    }
}
