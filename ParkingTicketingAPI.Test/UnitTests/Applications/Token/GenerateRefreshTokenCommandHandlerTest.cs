using Application.Applications.Token.Command;
using Application.Utilities.Helpers;
using ParkingTicketingAPI.Test.UnitTests.Applications.Token.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Token
{
    public sealed class GenerateRefreshTokenCommandHandlerTest : IClassFixture<TokenFixture>
    {
        private readonly TokenFixture _tokenFixture;

        public GenerateRefreshTokenCommandHandlerTest(TokenFixture tokenFixture)
        {
            _tokenFixture = tokenFixture;
        }

        [Fact(DisplayName = "Access token string should be returned when refresh flow succeeds")]
        public async Task GenerateRefreshTokenCommandHandlerSuccess()
        {
            // Arrange
            _tokenFixture.ResetMocks();
            _tokenFixture.SetupGenerateRefreshTokenSuccess();

            // Act
            var sut = new GenerateRefreshTokenCommandHandler(
                _tokenFixture.MockTokenService().Object,
                _tokenFixture.MockUserService().Object,
                _tokenFixture.MockEmployeeService().Object);

            var result = await sut.Handle(new GenerateRefreshTokenCommand(_tokenFixture.ValidGenerateRefreshTokenDTO()), CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("signed-access-token", result.Value);
            Assert.IsType<string>(result.Value);
            _tokenFixture.VerifyGenerateRefreshTokenSuccess();
        }

        [Fact(DisplayName = "Failure when user employee mapping is missing")]
        public async Task GenerateRefreshTokenCommandHandlerUserNotFound()
        {
            // Arrange
            _tokenFixture.ResetMocks();
            _tokenFixture.SetupGenerateRefreshTokenUserNotFound();

            // Act
            var sut = new GenerateRefreshTokenCommandHandler(
                _tokenFixture.MockTokenService().Object,
                _tokenFixture.MockUserService().Object,
                _tokenFixture.MockEmployeeService().Object);

            var dto = _tokenFixture.ValidGenerateRefreshTokenDTO();
            var result = await sut.Handle(new GenerateRefreshTokenCommand(dto), CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(GetError.NotFound(TokenFixture.TokenUserId), result.Error);
            _tokenFixture.VerifyGenerateRefreshTokenUserLookupOnly();
        }

        [Fact(DisplayName = "Failure when employee record is missing")]
        public async Task GenerateRefreshTokenCommandHandlerEmployeeNotFound()
        {
            // Arrange
            _tokenFixture.ResetMocks();
            _tokenFixture.SetupGenerateRefreshTokenEmployeeNotFound();

            // Act
            var sut = new GenerateRefreshTokenCommandHandler(
                _tokenFixture.MockTokenService().Object,
                _tokenFixture.MockUserService().Object,
                _tokenFixture.MockEmployeeService().Object);

            var result = await sut.Handle(new GenerateRefreshTokenCommand(_tokenFixture.ValidGenerateRefreshTokenDTO()), CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(GetError.NotFound(_tokenFixture.ExpectedEmployeeId), result.Error);
            _tokenFixture.VerifyGenerateRefreshTokenUserAndEmployeeLookup();
        }
    }
}
