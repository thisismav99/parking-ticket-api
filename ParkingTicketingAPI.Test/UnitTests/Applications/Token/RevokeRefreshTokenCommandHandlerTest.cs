using Application.Applications.Token.Command;
using ParkingTicketingAPI.Test.UnitTests.Applications.Token.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Token
{
    public sealed class RevokeRefreshTokenCommandHandlerTest : IClassFixture<TokenFixture>
    {
        private readonly TokenFixture _tokenFixture;

        public RevokeRefreshTokenCommandHandlerTest(TokenFixture tokenFixture)
        {
            _tokenFixture = tokenFixture;
        }

        [Fact(DisplayName = "Success when revoke completes")]
        public async Task RevokeRefreshTokenCommandHandlerSuccess()
        {
            // Arrange
            _tokenFixture.ResetMocks();
            _tokenFixture.SetupRevokeRefreshTokenSuccess();

            // Act
            var sut = new RevokeRefreshTokenCommandHandler(_tokenFixture.MockTokenService().Object);

            var result = await sut.Handle(new RevokeRefreshTokenCommand(_tokenFixture.ValidRevokeRefreshTokenDTO()), CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _tokenFixture.VerifyRevokeRefreshTokenSuccess();
        }

        [Fact(DisplayName = "Failure when token service revoke fails")]
        public async Task RevokeRefreshTokenCommandHandlerFailure()
        {
            // Arrange
            _tokenFixture.ResetMocks();
            _tokenFixture.SetupRevokeRefreshTokenFailure();

            // Act
            var sut = new RevokeRefreshTokenCommandHandler(_tokenFixture.MockTokenService().Object);

            var result = await sut.Handle(new RevokeRefreshTokenCommand(_tokenFixture.ValidRevokeRefreshTokenDTO()), CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("revoke failed", result.Error);
            _tokenFixture.VerifyRevokeRefreshTokenFailure();
        }
    }
}
