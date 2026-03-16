using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ShipsConfiguration : IEntityTypeConfiguration<Ship>
    {
        public void Configure(EntityTypeBuilder<Ship> builder)
        {
            builder
                .HasMany(s => s.tallySheets)
                .WithOne(ts => ts.Ship)
                .HasForeignKey(ts => ts.ShipId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
