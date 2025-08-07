using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Configurations;

public class WorldEntityConfiguration : IEntityTypeConfiguration<WorldEntity>
{
    public void Configure(EntityTypeBuilder<WorldEntity> builder)
    {
        builder.HasKey(e => new { e.Id });

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Ip).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Port).IsRequired();
        builder.Property(e => e.MaxCapacity).HasDefaultValue(100).IsRequired();
        builder.Property(w => w.RequiresPremium).IsRequired();
        builder.Property(w => w.TransferEnabled).IsRequired();
        builder.Property(w => w.AntiCheatEnabled).IsRequired();
        builder.Property(w => w.CreatedAt).IsRequired();

        builder.Property(w => w.Region)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(w => w.PvpType)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(w => w.WorldType)
            .HasConversion<string>()
            .HasMaxLength(50);
    }
}