using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    internal class tallySheetTruckConfiguration : IEntityTypeConfiguration<TallySheetTruck>
    {
        public void Configure(EntityTypeBuilder<TallySheetTruck> builder)
        {
            builder.HasKey(tst => new { tst.TallySheetId, tst.TruckId });

            builder.HasOne(tst => tst.TallySheet)
                   .WithMany(ts => ts.TallySheetTrucks)
                   .HasForeignKey(tst => tst.TallySheetId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(tst => tst.Truck)
                     .WithMany(t => t.TallySheetTrucks)
                     .HasForeignKey(tst => tst.TruckId)
                     .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
