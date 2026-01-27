using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasIndex(up => up.UserId)
                   .IsUnique();
            builder.HasOne(u => u.User)
                   .WithOne(up => up.profile)
                   .HasForeignKey<UserProfile>(up => up.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(up => up.Upload)
                   .WithOne(u  => u.profile)
                   .HasForeignKey<UserProfile>(up => up.UploadId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
