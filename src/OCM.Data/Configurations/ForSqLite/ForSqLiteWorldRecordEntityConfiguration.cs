using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Configurations.ForSqLite;

public class ForSqLiteWorldRecordEntityConfiguration : IEntityTypeConfiguration<WorldRecordEntity>
{
    public void Configure(EntityTypeBuilder<WorldRecordEntity> builder)
    {
        builder.HasKey(e => new { e.Id });

        builder.Property(e => e.Id).HasAnnotation("Sqlite:Autoincrement", true);
        builder.Property(e => e.WorldId).IsRequired();
        builder.Property(e => e.Record).IsRequired();
        builder.Property(e => e.CreatedAt);

        builder.HasOne(e => e.World)
            .WithMany(p => p.WorldRecords)
            .HasForeignKey(x => x.WorldId);
    }
}