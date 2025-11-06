using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Extensions
{
    internal static class EntityTypeBuilderExtension
    {
        public static void ApplyBaseEntityProperties(this EntityTypeBuilder builder)
        {
            builder.HasKey("Id");
            builder.Property<int>("Id").ValueGeneratedOnAdd();
            builder.Property<string>("CreatedBy").IsRequired().HasMaxLength(100);
            builder.Property<DateTime>("Created").IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property<string>("UpdatedBy").IsRequired(false).HasMaxLength(100);
            builder.Property<DateTime?>("Updated").IsRequired(false);
            builder.Property<bool>("IsActive").IsRequired();
        }
    }
}
