﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCM.Infrastructure.Entities;

namespace OCM.Infrastructure.Configurations;

public class IpBanEntityConfiguration : IEntityTypeConfiguration<IpBanEntity>
{
    public void Configure(EntityTypeBuilder<IpBanEntity> builder)
    {
        builder.ToTable("IpBans");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Ip)
            .IsRequired()
            .HasMaxLength(45);

        builder.Property(b => b.Reason)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(b => b.BannedAt)
            .IsRequired();

        builder.Property(b => b.ExpiresAt)
            .IsRequired();

        builder.Property(b => b.BannedBy)
            .IsRequired();

        builder.HasIndex(b => b.Ip)
            .HasDatabaseName("IX_Bans_Ip");

        builder.HasIndex(b => b.ExpiresAt)
            .HasDatabaseName("IX_Bans_ExpiresAt");
    }
}