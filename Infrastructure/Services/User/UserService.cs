using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.User;
using Infrastructure.Utilities.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Security.Claims;

namespace Infrastructure.Services.User
{
    internal class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly UserDbContext _userDbContext;
        private readonly DbSet<Domain.Entities.User.UserEmployee> _userEmployees;

        public UserService(UserManager<IdentityUser> userManager,
            UserDbContext userDbContext)
        {
            _userManager = userManager;
            _userDbContext = userDbContext;
            _userEmployees = _userDbContext.Set<Domain.Entities.User.UserEmployee>();
        }

        public async Task<Result<string>> AddUser(IdentityUser identityUser, 
            string password,
            Guid employeeId, 
            CancellationToken cancellationToken)
        {
            var user = await _userManager.CreateAsync(identityUser, password);
            
            if(user.Errors.Any())
            {
                var errors = user.Errors.Select(e => GetError.Error(e.Code, e.Description)).ToList();
                return Result.Failure<string>(string.Join(", ", errors));
            }

            var userEmployee = new Domain.Entities.User.UserEmployee
            {
                UserId = identityUser.Id,
                EmployeeId = employeeId
            };

            await _userEmployees.AddAsync(userEmployee, cancellationToken);
            await _userDbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(identityUser.Id);
        }

        public async Task<Result<string>> AddUserClaim(string email, string claimType, string claimValue)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is not null)
            {
                var userClaim = await _userManager.AddClaimAsync(user, new Claim(claimType, claimValue));

                if (userClaim.Errors.Any())
                {
                    var errors = userClaim.Errors.Select(e => GetError.Error(e.Code, e.Description)).ToList();
                    return Result.Failure<string>(string.Join(", ", errors));
                }

                return Result.Success(user.Id);
            }

            return Result.Failure<string>(GetError.Error(string.Empty, $"{email} not found"));
        }

        public async Task<Result<string>> AddUserRole(string email, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is not null)
            {
                var userRole = await _userManager.AddToRoleAsync(user, role);

                if (userRole.Errors.Any())
                {
                    var errors = userRole.Errors.Select(e => GetError.Error(e.Code, e.Description)).ToList();
                    return Result.Failure<string>(string.Join(", ", errors));
                }

                return Result.Success(user.Id);
            }

            return Result.Failure<string>(GetError.Error(string.Empty, $"{email} not found"));
        }

        public async Task<IdentityUser?> GetIdentityUserByEmail(string email, CancellationToken cancellationToken)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<List<IdentityUser>> GetIdentityUsers(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await GetPagedList<IdentityUser>.GetList(_userManager.Users, pageNumber, pageSize, cancellationToken);
        }
    }
}
