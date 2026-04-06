using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.User
{
    internal class User : IdentityUser
    {
        public Guid EmployeeId { get; set; }
    }
}
