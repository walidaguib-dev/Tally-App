using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class ObservationsConfiguration : IEntityTypeConfiguration<Observation>
    {
        public void Configure(EntityTypeBuilder<Observation> builder)
        {
            builder.Property(o => o.Type).HasConversion<string>().IsRequired();
        }
    }
}
