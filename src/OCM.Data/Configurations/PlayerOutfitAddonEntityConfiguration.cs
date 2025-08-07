﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Configurations;

public class PlayerOutfitAddonEntityConfiguration : IEntityTypeConfiguration<PlayerOutfitAddonEntity>
{
    public void Configure(EntityTypeBuilder<PlayerOutfitAddonEntity> builder)
    {
        builder.HasKey(e => new { e.PlayerId, e.LookType, e.AddonLevel });

        builder.Property(e => e.LookType).IsRequired();
        builder.Property(e => e.PlayerId).IsRequired();
        builder.Property(e => e.AddonLevel).IsRequired();
    }
}