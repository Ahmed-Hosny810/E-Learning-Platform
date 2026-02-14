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
    public class LessonProgressConfiguration : IEntityTypeConfiguration<LessonProgress>
    {
        public void Configure(EntityTypeBuilder<LessonProgress> builder)
        {
            builder.ToTable("LessonProgress");

            builder.HasKey(lp => lp.Id);

            builder.Property(lp => lp.UserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(lp => lp.CompletionPercentage)
                .HasPrecision(5, 2)
                .HasDefaultValue(0);

            builder.Property(lp => lp.TimeSpentSeconds)
                .HasDefaultValue(0);

            builder.Property(lp => lp.LastAccessedAt);

            builder.HasIndex(lp => new { lp.UserId, lp.LessonId })
                .IsUnique();

            builder.HasIndex(lp => lp.LessonId);

            builder.HasOne(lp => lp.Lesson)
                .WithMany()
                .HasForeignKey(lp => lp.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
