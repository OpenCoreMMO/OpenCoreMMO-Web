using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Configurations;

public class GuildRankEntityConfiguration : IEntityTypeConfiguration<GuildRankEntity>
{
    public void Configure(EntityTypeBuilder<GuildRankEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.GuildId);
        builder.Property(e => e.Name).HasMaxLength(100);
        builder.Property(e => e.Level);

        builder.HasOne(x => x.Guild)
            .WithMany(x => x.Ranks)
            .HasForeignKey(x => x.GuildId);
    }
}