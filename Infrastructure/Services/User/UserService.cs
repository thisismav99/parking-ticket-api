using Infrastructure.Interfaces.User;
using Infrastructure.Utilities.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

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

        public async Task<(string?, List<string>?)> AddUser(IdentityUser identityUser, 
            string password,
            Guid employeeId, 
            CancellationToken cancellationToken)
        {
            var user = await _userManager.CreateAsync(identityUser, password);
            
            if(user.Errors.Any())
            {
                return (null, user.Errors.Select(e => $"Error Code: {e.Code} - {e.Description}").ToList());
            }

            var userEmployee = new Domain.Entities.User.UserEmployee
            {
                UserId = identityUser.Id,
                EmployeeId = employeeId
            };

            await _userEmployees.AddAsync(userEmployee, cancellationToken);
            await _userDbContext.SaveChangesAsync(cancellationToken);

            return (identityUser.Id, null);
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
