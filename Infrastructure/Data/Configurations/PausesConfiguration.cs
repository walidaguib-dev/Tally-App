using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class PausesConfiguration : IEntityTypeConfiguration<Pause>
    {
        public void Configure(EntityTypeBuilder<Pause> builder)
        {
            builder.Property(p => p.Reason).HasConversion<string>().IsRequired();
        }
    }
}
