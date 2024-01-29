using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WineInfo.Entities;

namespace WineInfo.DAL.Persistence
{
    public class MesurementConfiguration
           : IEntityTypeConfiguration<Mesurement>
    {

        public void Configure(EntityTypeBuilder<Mesurement> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Observations)
                .HasMaxLength(2000);

            builder.Property(e => e.Variety)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(e => e.WineType)
                .HasConversion<int>()
                .IsRequired();
        }
    }
}
