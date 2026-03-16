using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasIndex(x => x.userId).IsUnique();
            builder
                .HasOne(rt => rt.user)
                .WithOne(u => u.refreshToken)
                .HasForeignKey<RefreshToken>(rt => rt.userId)
                .OnDelete(DeleteBehavior.Cascade);
            // Composite index for fast lookups
            builder
                .HasIndex(rt => new { rt.userId, rt.Token })
                .HasDatabaseName("IX_RefreshTokens_UserId_Token");
            builder.HasIndex(rt => rt.ExpiresAt);

            builder.HasQueryFilter(rt => rt.ExpiresAt > DateTime.UtcNow);
        }
    }
}
