using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ClientEntityConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder
                .HasOne(c => c.Merchandise)
                .WithMany()
                .HasForeignKey(c => c.MerchandiseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
