using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Extensions;

namespace Persistence.Configurations.Customer
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Domain.Entities.Customer.Customer>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Customer.Customer> builder)
        {
            builder.ApplyBaseEntityProperties();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.MiddleName).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ContactNo).IsRequired(false).HasMaxLength(15);
            builder.Property(x => x.Email).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.AddressId).IsRequired();

            builder.HasOne(x => x.Address).WithMany().HasForeignKey(x => x.AddressId).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Customers", "customer");
        }
    }
}
