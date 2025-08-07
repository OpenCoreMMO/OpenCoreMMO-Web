﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCM.Infrastructure.Entities;
using OCM.Infrastructure.Extensions;

namespace OCM.Infrastructure.Configurations;

public class PlayerInventoryItemEntityConfiguration : IEntityTypeConfiguration<PlayerInventoryItemEntity>
{
    public void Configure(EntityTypeBuilder<PlayerInventoryItemEntity> entity)
    {
        entity.HasKey(e => e.Id);

        entity.ToTable("PlayerInventoryItem");

        entity.HasIndex(e => new { e.PlayerId, e.SlotId }).IsUnique();

        entity.Property(e => e.PlayerId)
            .IsRequired()
            .HasColumnType("int");

        entity.Property(e => e.ServerId)
            .IsRequired()
            .HasColumnType("int");

        entity.Property(e => e.SlotId)
            .IsRequired()
            .HasColumnType("int");

        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        entity.Property(e => e.Amount)
            .HasColumnType("smallint")
            .HasDefaultValueSql("1");

        entity.Property(e => e.Attributes)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonExtensions.SerializeAllAttributes(v),
                v => JsonExtensions.DeserializeAllAttributes(v)
            );

        entity.HasOne(d => d.Player)
            .WithMany(p => p.PlayerInventoryItems)
            .HasForeignKey(d => d.PlayerId)
            .HasConstraintName("player_inventory_items_ibfk_1");
    }
}