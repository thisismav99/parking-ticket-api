using Application.Applications.User.DTO;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.User;
using Microsoft.AspNetCore.Identity;
using Moq;
using ParkingTicketingAPI.Test.UnitTests.Fixtures;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.User.Fixture
{
    public class UserFixture : BaseFixture
    {
        public const string FoundEmail = "found@test.com";

        public const string MissingEmail = "missing@test.com";

        private readonly Mock<IUserService> _mockUserService;

        public UserFixture()
        {
            _mockUserService = new Mock<IUserService>(MockBehavior.Strict);
        }

        internal Mock<IUserService> MockUserService() => _mockUserService;

        public Guid GetEmployeeIdForAddUser() => guid;

        #region AddUserCommandHandler
        public AddUserDTO ValidAddUserDTO()
        {
            return new AddUserDTO()
            {
                Email = "newuser@test.com",
                PhoneNumber = "+639000000000",
                Password = "Password1!",
                EmployeeId = guid
            };
        }

        public void SetupAddUser()
        {
            _mockUserService.Setup(u => u.AddUser(It.IsAny<IdentityUser>(), It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success("new-identity-id"));
        }

        public void VerifyAddUser()
        {
            _mockUserService.Verify(u => u.AddUser(It.IsAny<IdentityUser>(), It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion

        #region GetUsersQueryHandler
        internal List<IdentityUser> ValidIdentityUsers()
        {
            return new List<IdentityUser>()
            {
                new IdentityUser
                {
                    Id = "id-1",
                    Email = "a@test.com",
                    UserName = "a@test.com",
                    PhoneNumber = "+111"
                },
                new IdentityUser
                {
                    Id = "id-2",
                    Email = "b@test.com",
                    UserName = "b@test.com",
                    PhoneNumber = "+222"
                },
                new IdentityUser
                {
                    Id = "id-3",
                    Email = "c@test.com",
                    UserName = "c@test.com",
                    PhoneNumber = "+333"
                }
            };
        }

        public void SetupGetUsers()
        {
            _mockUserService.Setup(u => u.GetIdentityUsers(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ValidIdentityUsers());
        }

        public void VerifyGetUsers()
        {
            _mockUserService.Verify(u => u.GetIdentityUsers(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion

        #region GetUserByEmailQueryHandler
        internal IdentityUser ValidIdentityUserByEmail()
        {
            return new IdentityUser
            {
                Id = "found-id",
                Email = FoundEmail,
                UserName = FoundEmail,
                PhoneNumber = "+999"
            };
        }

        public void SetupGetUserByEmailFound()
        {
            _mockUserService.Setup(u => u.GetIdentityUserByEmail(FoundEmail, It.IsAny<CancellationToken>()))
                .ReturnsAsync(ValidIdentityUserByEmail());
        }

        public void SetupGetUserByEmailMissing()
        {
            _mockUserService.Setup(u => u.GetIdentityUserByEmail(MissingEmail, It.IsAny<CancellationToken>()))
                .ReturnsAsync((IdentityUser?)null);
        }

        public void VerifyGetUserByEmailFound()
        {
            _mockUserService.Verify(u => u.GetIdentityUserByEmail(FoundEmail, It.IsAny<CancellationToken>()), Times.Once);
        }

        public void VerifyGetUserByEmailMissing()
        {
            _mockUserService.Verify(u => u.GetIdentityUserByEmail(MissingEmail, It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion
    }
}
