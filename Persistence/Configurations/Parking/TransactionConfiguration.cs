using Domain.Entities.Parking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Extensions;

namespace Persistence.Configurations.Parking
{
    internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ApplyBaseEntityProperties();
            builder.Property(x => x.AmountToPay).IsRequired().HasPrecision(18, 2);
            builder.Property(x => x.AmountPaid).IsRequired().HasPrecision(18, 2);
            builder.Property(x => x.AmountChange)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasComputedColumnSql("[AmountToPay] - [AmountPaid]", stored: false);
            builder.Property(x => x.IsCard).IsRequired();
            builder.Property(x => x.IsCash).IsRequired();

            builder.ToTable("Transactions", "parking");
        }
    }
}
