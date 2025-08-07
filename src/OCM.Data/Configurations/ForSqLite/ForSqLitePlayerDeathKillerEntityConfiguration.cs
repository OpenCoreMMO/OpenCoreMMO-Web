using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Configurations.ForSqLite;

public class ForSqLitePlayerDeathKillerEntityConfiguration : IEntityTypeConfiguration<PlayerDeathKillerEntity>
{
    public void Configure(EntityTypeBuilder<PlayerDeathKillerEntity> builder)
    {
        builder.ToTable("PlayerDeathKiller");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasAnnotation("Sqlite:Autoincrement", true)
            .IsRequired();

        builder.Property(e => e.PlayerId);
        builder.Property(e => e.Damage).IsRequired();
        builder.Property(e => e.KillerName).HasMaxLength(200).IsRequired();
        builder.Property(e => e.PlayerDeathId).IsRequired();

        builder.HasOne(x => x.PlayerDeath)
            .WithMany(p => p.Killers)
            .HasForeignKey(d => d.PlayerDeathId)
            .HasConstraintName("player_death_ibfk_1");

        builder.HasOne(x => x.Player).WithMany().HasForeignKey(d => d.PlayerId)
            .HasConstraintName("player_ibfk_1");
    }
}