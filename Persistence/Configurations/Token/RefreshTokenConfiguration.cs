using Domain.Entities.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Token
{
    internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Token).IsRequired().HasMaxLength(200);
            builder.Property(x => x.IsExpired)
                .IsRequired()
                .HasComputedColumnSql("CASE WHEN [DateExpires] <= SYSUTCDATETIME() THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END");
            builder.Property(x => x.DateExpires)
                .IsRequired()
                .HasDefaultValueSql("DATEADD(DAY, 7, SYSUTCDATETIME())");
            builder.Property(x => x.IsRevoked).IsRequired();
            builder.Property(x => x.DateRevoked).IsRequired(false);
            builder.Property(x => x.UserId).IsRequired();

            builder.HasOne(x => x.IdentityUser).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("RefreshTokens", "user");
        }
    }
}
