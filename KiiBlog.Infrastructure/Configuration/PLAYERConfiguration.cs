using KiiBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiiBlog.Infrastructure.Configuration
{
    public class PLAYERConfiguration : IEntityTypeConfiguration<PLAYER>
    {
        public void Configure(EntityTypeBuilder<PLAYER> builder)
        {
            // Table name
            builder.ToTable(nameof(PLAYER));

            // Primary key
            builder.HasKey(f => f.PLAYER_ID);

            // Properties
            builder.Property(f => f.PLAYER_NO)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(f => f.PLAYER_NAME)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(f => f.PLAYER_PROFILE)
                .HasMaxLength(3000);

            builder.Property(f => f.CONTRACT_TYPE_ID);

            builder.Property(f => f.CONTRACT_TYPE_CODE)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(f => f.CONTRACT_TYPE_NAME)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(f => f.TRANSFER_STATUS_ID);

            builder.Property(f => f.TRANSFER_STATUS_CODE)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(f => f.TRANSFER_STATUS_NAME)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(f => f.ROW_UN)
                .HasDefaultValueSql("NEWID()");

            // Index
            builder.HasIndex(f => f.PLAYER_NAME);

            // Relationships
        }
    }
}
