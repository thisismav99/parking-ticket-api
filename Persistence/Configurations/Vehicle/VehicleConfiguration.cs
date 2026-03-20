using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Extensions;

namespace Persistence.Configurations.Vehicle
{
    internal class VehicleConfiguration : IEntityTypeConfiguration<Domain.Entities.Vehicle.Vehicle>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Vehicle.Vehicle> builder)
        {
            builder.ApplyBaseEntityProperties();
            builder.Property(x => x.PlateNo).IsRequired().HasMaxLength(10);
            builder.Property(x => x.Brand).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Color).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.Model).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.IsElectric).IsRequired();
            builder.Property(x => x.IsHybrid).IsRequired();
            builder.Property(x => x.CustomerId).IsRequired(false);

            builder.HasOne(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.SetNull);

            builder.ToTable("Vehicles", "vehicle");
        }
    }
}
