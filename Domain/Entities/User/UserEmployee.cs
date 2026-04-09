using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.User
{
    internal class UserEmployee
    {
        public required string UserId { get; set; }
        public Guid EmployeeId { get; set; }

        public IdentityUser? IdentityUser { get; set; }
    }
}
