using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.User
{
    internal class UserEmployeeConfiguration : IEntityTypeConfiguration<Domain.Entities.User.UserEmployee>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.User.UserEmployee> builder)
        {
            builder.HasNoKey();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.EmployeeId).IsRequired(false);

            builder.HasOne(x => x.IdentityUser).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("UserEmployees", "user");
        }
    }
}
