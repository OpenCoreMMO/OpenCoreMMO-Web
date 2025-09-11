using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Configurations;

public class GuildEntityConfiguration : IEntityTypeConfiguration<GuildEntity>
{
    public void Configure(EntityTypeBuilder<GuildEntity> builder)
    {
        builder.HasKey(e => new { e.Id });

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).HasMaxLength(100);
        builder.Property(e => e.OwnerId);
        builder.Property(e => e.CreatedAt).HasDefaultValue(DateTime.UtcNow);
        builder.Property(e => e.Modt);
        builder.Property(e => e.BankAmount).HasDefaultValue(0);
        builder.Property(e => e.Description).HasMaxLength(500);
        builder.Property(e => e.Level).HasDefaultValue(1);
        builder.Property(e => e.Points).HasDefaultValue(0);

        builder.HasMany(x => x.Members).WithOne().HasForeignKey(x => x.GuildId);
        builder.HasMany(x => x.Ranks).WithOne().HasForeignKey(x => x.GuildId);
    }
}