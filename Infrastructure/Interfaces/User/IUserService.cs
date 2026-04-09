using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Interfaces.User
{
    internal interface IUserService
    {
        Task<(string?, List<string>?)> AddUser(IdentityUser identityUser, 
            string password, 
            Guid employeeId, 
            CancellationToken cancellationToken);

        Task<IdentityUser?> GetIdentityUserByEmail(string email, CancellationToken cancellationToken);

        Task<List<IdentityUser>> GetIdentityUsers(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
