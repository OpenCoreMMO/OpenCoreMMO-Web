using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Configurations.ForSqLite;

public class ForSqLiteTownEntityConfiguration : IEntityTypeConfiguration<TownEntity>
{
    public void Configure(EntityTypeBuilder<TownEntity> builder)
    {
        builder.HasKey(e => new { e.Id });

        builder.Property(e => e.Id).HasAnnotation("Sqlite:Autoincrement", true);
        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
        builder.Property(e => e.WorldId).IsRequired();
    }
}