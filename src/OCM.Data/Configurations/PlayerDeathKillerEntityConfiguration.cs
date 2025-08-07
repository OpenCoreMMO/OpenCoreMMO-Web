using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Configurations;

public class PlayerDeathKillerEntityConfiguration : IEntityTypeConfiguration<PlayerDeathKillerEntity>
{
    public void Configure(EntityTypeBuilder<PlayerDeathKillerEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(e => e.PlayerId);
        builder.Property(e => e.Damage).IsRequired();
        builder.Property(e => e.KillerName).HasMaxLength(200).IsRequired();
        builder.Property(e => e.PlayerDeathId).IsRequired();

        builder.HasOne(x => x.PlayerDeath).WithMany(x => x.Killers);

        builder.HasOne(x => x.Player).WithMany().HasForeignKey(d => d.PlayerId);
    }
}