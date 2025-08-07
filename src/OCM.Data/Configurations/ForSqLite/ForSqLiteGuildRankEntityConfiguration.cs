using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Configurations.ForSqLite;

public class ForSqLiteGuildRankEntityConfiguration : IEntityTypeConfiguration<GuildRankEntity>
{
    public void Configure(EntityTypeBuilder<GuildRankEntity> builder)
    {
        builder.HasKey(e => new { e.Id });

        builder.Property(e => e.Id).HasAnnotation("Sqlite:Autoincrement", true);
        builder.Property(e => e.GuildId);
        builder.Property(e => e.Name).HasMaxLength(100);
        builder.Property(e => e.Level);

        builder.HasOne(x => x.Guild);
    }
}