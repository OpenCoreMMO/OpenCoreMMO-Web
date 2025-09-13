using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Configurations;

public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .ValueGeneratedOnAdd();

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasIndex(r => r.Name)
            .IsUnique()
            .HasDatabaseName("IX_Roles_Name");

        Seed(builder);
    }

    private static void Seed(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasData(
            new RoleEntity
            {
                Id = 1,
                Name = "Player",
                Description = "Basic player role with standard game access"
            },
            new RoleEntity
            {
                Id = 2,
                Name = "Site Editor",
                Description = "Role for managing website content and basic administrative tasks"
            },
            new RoleEntity
            {
                Id = 3,
                Name = "Gamemaster",
                Description = "Advanced role for game moderation and player support"
            },
            new RoleEntity
            {
                Id = 4,
                Name = "Administrator",
                Description = "Full administrative access with system management capabilities"
            }
        );
    }
}