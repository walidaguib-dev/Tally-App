using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    internal class UploadsConfiguration : IEntityTypeConfiguration<Uploads>
    {
        public void Configure(EntityTypeBuilder<Uploads> builder)
        {
            builder.HasOne(u => u.User)
                   .WithOne(u => u.Upload)
                   .HasForeignKey<Uploads>(u => u.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
