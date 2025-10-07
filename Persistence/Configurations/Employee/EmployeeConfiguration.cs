using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Extensions;

namespace Persistence.Configurations.Employee
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Domain.Entities.Employee.Employee>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Employee.Employee> builder)
        {
            builder.ApplyBaseEntityProperties();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.MiddleName).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.AddressId).IsRequired();
            builder.Property(x => x.CompanyId).IsRequired();

            builder.HasOne(x => x.Address).WithMany().HasForeignKey(x => x.AddressId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("emp.Employees");
        }
    }
}
