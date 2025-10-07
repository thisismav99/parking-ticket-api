using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Extensions;

namespace Persistence.Configurations.Company
{
    internal class CompanyConfiguration : IEntityTypeConfiguration<Domain.Entities.Company.Company>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Company.Company> builder)
        {
            builder.ApplyBaseEntityProperties();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(500);

            builder.ToTable("cmp.Company");
        }
    }
}
