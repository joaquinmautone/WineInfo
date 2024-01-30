using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WineInfo.Entities;

namespace WineInfo.DAL.Persistence
{
    public class UserConfiguration
           : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.UserId);

            builder.HasIndex(u => u.UserName)
                .IsUnique();

            builder.Property(p => p.UserName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Password)
                .IsRequired();
        }
    }
}
