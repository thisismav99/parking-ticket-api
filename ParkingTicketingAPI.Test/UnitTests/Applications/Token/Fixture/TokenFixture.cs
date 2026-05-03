using CSharpFunctionalExtensions;
using Domain.Entities.Token;
using Domain.Entities.User;
using Infrastructure.Interfaces.Employee;
using Infrastructure.Interfaces.Token;
using Infrastructure.Interfaces.User;
using Microsoft.AspNetCore.Identity;
using Moq;
using ParkingTicketingAPI.Test.UnitTests.Fixtures;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Token.Fixture
{
    public class TokenFixture : BaseFixture
    {
        public const string TokenUserId = "token-user-1";

        public const string OldRefreshTokenValue = "old-refresh-token";

        private readonly Mock<ITokenService> _mockTokenService;

        private readonly Mock<IUserService> _mockUserService;

        private readonly Mock<IEmployeeService> _mockEmployeeService;

        public TokenFixture()
        {
            _mockTokenService = new Mock<ITokenService>(MockBehavior.Strict);
            _mockUserService = new Mock<IUserService>(MockBehavior.Strict);
            _mockEmployeeService = new Mock<IEmployeeService>(MockBehavior.Strict);
        }

        public void ResetMocks()
        {
            _mockTokenService.Reset();
            _mockUserService.Reset();
            _mockEmployeeService.Reset();
        }

        internal Mock<ITokenService> MockTokenService() => _mockTokenService;

        internal Mock<IUserService> MockUserService() => _mockUserService;

        internal Mock<IEmployeeService> MockEmployeeService() => _mockEmployeeService;

        public Guid ExpectedEmployeeId => guid;

        internal Domain.Entities.Employee.Employee ValidEmployeeForToken()
        {
            return new Domain.Entities.Employee.Employee("Tok",
                null,
                "En",
                guid,
                invalidGuid,
                "User 1",
                true);
        }

        internal UserEmployee ValidUserEmployee()
        {
            return new UserEmployee
            {
                UserId = TokenUserId,
                EmployeeId = guid,
                IdentityUser = new IdentityUser
                {
                    Id = TokenUserId,
                    Email = "tok@test.com",
                    UserName = "tok@test.com"
                }
            };
        }

        #region GenerateRefreshTokenCommandHandler
        public Application.Applications.Token.DTO.GenerateRefreshTokenDTO ValidGenerateRefreshTokenDTO()
        {
            return new Application.Applications.Token.DTO.GenerateRefreshTokenDTO
            {
                RefreshToken = OldRefreshTokenValue,
                UserId = TokenUserId
            };
        }

        public void SetupGenerateRefreshTokenSuccess()
        {
            _mockUserService.Setup(u => u.GetUserEmployee(TokenUserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(ValidUserEmployee());

            _mockEmployeeService.Setup(e => e.GetEmployeeById(It.Is<Guid>(id => id == guid), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ValidEmployeeForToken());

            _mockTokenService.Setup(t => t.RevokeRefreshToken(OldRefreshTokenValue, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success());

            _mockTokenService.Setup(t => t.GenerateRefreshToken(It.IsAny<RefreshToken>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(string.Empty);

            _mockTokenService.Setup(t => t.GenerateToken(It.IsAny<IdentityUser>(), It.IsAny<Domain.Entities.Employee.Employee>()))
                .ReturnsAsync("signed-access-token");
        }

        public void VerifyGenerateRefreshTokenSuccess()
        {
            _mockUserService.Verify(u => u.GetUserEmployee(TokenUserId, It.IsAny<CancellationToken>()), Times.Once);
            _mockEmployeeService.Verify(e => e.GetEmployeeById(It.Is<Guid>(id => id == guid), It.IsAny<CancellationToken>()), Times.Once);
            _mockTokenService.Verify(t => t.RevokeRefreshToken(OldRefreshTokenValue, It.IsAny<CancellationToken>()), Times.Once);
            _mockTokenService.Verify(t => t.GenerateRefreshToken(It.IsAny<RefreshToken>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockTokenService.Verify(t => t.GenerateToken(It.IsAny<IdentityUser>(), It.IsAny<Domain.Entities.Employee.Employee>()), Times.Once);
        }

        public void SetupGenerateRefreshTokenUserNotFound()
        {
            _mockUserService.Setup(u => u.GetUserEmployee(TokenUserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((UserEmployee?)null);
        }

        public void VerifyGenerateRefreshTokenUserLookupOnly()
        {
            _mockUserService.Verify(u => u.GetUserEmployee(TokenUserId, It.IsAny<CancellationToken>()), Times.Once);
        }

        public void SetupGenerateRefreshTokenEmployeeNotFound()
        {
            _mockUserService.Setup(u => u.GetUserEmployee(TokenUserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(ValidUserEmployee());

            _mockEmployeeService.Setup(e => e.GetEmployeeById(It.Is<Guid>(id => id == guid), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.Employee.Employee?)null);
        }

        public void VerifyGenerateRefreshTokenUserAndEmployeeLookup()
        {
            _mockUserService.Verify(u => u.GetUserEmployee(TokenUserId, It.IsAny<CancellationToken>()), Times.Once);
            _mockEmployeeService.Verify(e => e.GetEmployeeById(It.Is<Guid>(id => id == guid), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion

        #region RevokeRefreshTokenCommandHandler
        public Application.Applications.Token.DTO.RevokeRefreshTokenDTO ValidRevokeRefreshTokenDTO()
        {
            return new Application.Applications.Token.DTO.RevokeRefreshTokenDTO
            {
                RefreshToken = "revoke-this-token"
            };
        }

        public void SetupRevokeRefreshTokenSuccess()
        {
            _mockTokenService.Setup(t => t.RevokeRefreshToken(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success());
        }

        public void VerifyRevokeRefreshTokenSuccess()
        {
            _mockTokenService.Verify(t => t.RevokeRefreshToken(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        public void SetupRevokeRefreshTokenFailure()
        {
            _mockTokenService.Setup(t => t.RevokeRefreshToken(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure("revoke failed"));
        }

        public void VerifyRevokeRefreshTokenFailure()
        {
            _mockTokenService.Verify(t => t.RevokeRefreshToken(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion
    }
}
