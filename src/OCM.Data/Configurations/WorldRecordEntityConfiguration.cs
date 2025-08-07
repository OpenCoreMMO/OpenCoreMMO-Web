﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Configurations;

public class WorldRecordEntityConfiguration : IEntityTypeConfiguration<WorldRecordEntity>
{
    public void Configure(EntityTypeBuilder<WorldRecordEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Record).IsRequired();
        builder.Property(e => e.CreatedAt);

        builder.HasOne(e => e.World)
            .WithMany(p => p.WorldRecords)
            .HasForeignKey(x => x.WorldId).IsRequired();
    }
}