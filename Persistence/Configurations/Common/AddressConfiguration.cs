using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Extensions;

namespace Persistence.Configurations.Common
{
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ApplyBaseEntityProperties();
            builder.Property(x => x.LotNo).IsRequired(false);
            builder.Property(x => x.Street).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Barangay).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Municipality).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Region).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Country).IsRequired().HasMaxLength(100);

            builder.ToTable("cmn.Address");
        }
    }
}
