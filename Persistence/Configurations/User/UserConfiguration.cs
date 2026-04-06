using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.User
{
    internal class UserConfiguration : IEntityTypeConfiguration<Domain.Entities.User.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.User.User> builder)
        {
            builder.Property(x => x.EmployeeId).IsRequired();

            builder.ToTable("Users", "user");
        }
    }
}
