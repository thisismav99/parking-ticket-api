using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Utilities.Extensions;

namespace Persistence.Configurations.Parking
{
    internal class ParkingConfiguration : IEntityTypeConfiguration<Domain.Entities.Parking.Parking>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Parking.Parking> builder)
        {
            builder.ApplyBaseEntityProperties();
            builder.Property(x => x.ParkDateTime).IsRequired();
            builder.Property(x => x.ExitDateTime).IsRequired(false);
            builder.Property(x => x.TotalHours)
                .IsRequired()
                .HasComputedColumnSql("DATEDIFF(SECOND, [ParkDateTime], [ExitDateTime]) / 3600.0", stored: false);
            builder.Property(x => x.VehicleId).IsRequired();
            builder.Property(x => x.EmployeeId).IsRequired();

            builder.HasOne(x => x.Vehicle).WithMany().HasForeignKey(x => x.VehicleId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Employee).WithMany().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Parkings", "parking");
        }
    }
}
