using E_learningPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.Contexts.DbConfigurations
{
    public class SectionProgressConfiguration : IEntityTypeConfiguration<SectionProgress>
    {
        public void Configure(EntityTypeBuilder<SectionProgress> builder)
        {
            builder.ToTable("SectionProgress");

            builder.HasKey(mp => mp.Id);

            builder.Property(mp => mp.UserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(mp => mp.CompletionPercentage)
                .HasPrecision(5, 2)
                .HasDefaultValue(0);

            builder.Property(mp => mp.CompletedLessonsCount)
                .HasDefaultValue(0);

            builder.Property(mp => mp.TotalLessonsCount)
                .HasDefaultValue(0);

            builder.Property(mp => mp.LastUpdatedAt);

            builder.HasIndex(mp => new { mp.UserId, mp.SectionId })
                .IsUnique();

            builder.HasIndex(mp => mp.SectionId);

            builder.HasOne(mp => mp.Section)
                .WithMany()
                .HasForeignKey(mp => mp.SectionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
