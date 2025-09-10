using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Configurations;

public class TownEntityConfiguration : IEntityTypeConfiguration<TownEntity>
{
    public void Configure(EntityTypeBuilder<TownEntity> builder)
    {
        builder.HasKey(e => new { e.Id });

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
        builder.Property(e => e.WorldId).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();

        builder.HasOne(e => e.World)
            .WithMany()
            .HasForeignKey(e => e.WorldId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}