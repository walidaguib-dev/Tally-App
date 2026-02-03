using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class TallySheetsConfiguration : IEntityTypeConfiguration<TallySheet>
    {
        public void Configure(EntityTypeBuilder<TallySheet> builder)
        {
            builder.Property(t => t.Shift)
                .HasConversion<string>()
                .IsRequired();
            builder.Property(t => t.Zone)
                .HasConversion<string>()
                .IsRequired();

        }
    }
}
