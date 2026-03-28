using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class CarsEntityConfiguration : IEntityTypeConfiguration<Cars>
    {
        public void Configure(EntityTypeBuilder<Cars> builder)
        {
            builder.Property(p => p.carStatus).HasConversion<string>().IsRequired();

            builder.HasIndex(v => v.VinNumber).IsUnique();
            builder.HasIndex(v => v.TallySheetId);
        }
    }
}
