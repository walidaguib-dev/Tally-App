using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class EmailTokenConfiguration : IEntityTypeConfiguration<EmailToken>
    {
        public void Configure(EntityTypeBuilder<EmailToken> builder)
        {
            // Email verifications
            builder
                .HasIndex(e => e.CodeHash)
                .IsUnique();
            builder
                .Property(e => e.Purpose)
                .HasConversion<string>();
        }
    }
}
