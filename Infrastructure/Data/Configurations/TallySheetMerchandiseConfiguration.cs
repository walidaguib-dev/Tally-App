using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    internal class TallySheetMerchandiseConfiguration : IEntityTypeConfiguration<TallySheetMerchandise>
    {
        public void Configure(EntityTypeBuilder<TallySheetMerchandise> builder)
        {
            builder.HasKey(tsm => new { tsm.TallySheetId, tsm.MerchandiseId });

            builder.HasOne(tsm => tsm.Merchandise)
                   .WithMany(ts => ts.TallySheetMerchandises)
                   .HasForeignKey(tsm => tsm.MerchandiseId);

            builder.HasOne(tsm => tsm.TallySheet)
                     .WithMany(t => t.TallySheetMerchandises)
                     .HasForeignKey(tsm => tsm.TallySheetId);
        }
    }
}
