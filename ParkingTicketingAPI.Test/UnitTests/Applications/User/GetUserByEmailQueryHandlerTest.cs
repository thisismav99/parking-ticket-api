using Application.Applications.User.DTO;
using Application.Applications.User.Query;
using Application.Utilities.Helpers;
using Mapster;
using ParkingTicketingAPI.Test.UnitTests.Applications.User.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.User
{
    public sealed class GetUserByEmailQueryHandlerTest : IClassFixture<UserFixture>
    {
        private readonly UserFixture _userFixture;

        public GetUserByEmailQueryHandlerTest(UserFixture userFixture)
        {
            _userFixture = userFixture;
        }

        [Theory(DisplayName = "User should be returned when email exists and not found when missing")]
        [InlineData(UserFixture.FoundEmail)]
        [InlineData(UserFixture.MissingEmail)]
        public async Task GetUserByEmailQueryHandlerReturns(string email)
        {
            // Arrange
            _userFixture.SetupGetUserByEmailFound();
            _userFixture.SetupGetUserByEmailMissing();

            // Act
            var sut = new GetUserByEmailQueryHandler(_userFixture.MockUserService().Object);

            var result = await sut.Handle(new GetUserByEmailQuery(email), CancellationToken.None);
            result.Adapt<ResponseUserDTO>();

            // Assert
            if (email == UserFixture.FoundEmail)
            {
                Assert.True(result.IsSuccess);
                Assert.NotNull(result.Value);
                Assert.IsType<ResponseUserDTO>(result.Value);
                _userFixture.VerifyGetUserByEmailFound();
            }
            else
            {
                Assert.True(result.IsFailure);
                Assert.Equal(GetError.NotFound(email), result.Error);
                Assert.IsType<string>(result.Error);
                _userFixture.VerifyGetUserByEmailMissing();
            }
        }
    }
}
