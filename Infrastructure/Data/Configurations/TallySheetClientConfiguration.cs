using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class TallySheetMerchandiseConfiguration : IEntityTypeConfiguration<TallySheetClient>
    {
        public void Configure(EntityTypeBuilder<TallySheetClient> builder)
        {
            builder.HasKey(tsm => new { tsm.TallySheetId, tsm.ClientId });

            builder
                .HasOne(tsm => tsm.Client)
                .WithMany(ts => ts.TallySheetClients)
                .HasForeignKey(tsm => tsm.ClientId);

            builder
                .HasOne(tsm => tsm.TallySheet)
                .WithMany(t => t.TallySheetClients)
                .HasForeignKey(tsm => tsm.TallySheetId);
        }
    }
}
