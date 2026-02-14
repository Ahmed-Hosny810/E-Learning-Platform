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
    public class LessonContentConfiguration : IEntityTypeConfiguration<LessonContent>
    {
        public void Configure(EntityTypeBuilder<LessonContent> builder)
        {
            builder.ToTable("LessonContents");

            builder.HasKey(lc => lc.Id);

            builder.Property(lc => lc.ContentType)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(lc => lc.ContentUrl)
                .HasMaxLength(500);

            builder.Property(lc => lc.TextContent)
                .HasColumnType("nvarchar(max)");

            builder.Property(lc => lc.DisplayOrder)
                .IsRequired();

            builder.Property(lc => lc.IsDownloadable)
                .HasDefaultValue(false);

            builder.HasIndex(lc => new { lc.LessonId, lc.DisplayOrder });

            builder.HasOne(lc => lc.Lesson)
                .WithMany(l => l.LessonContents)
                .HasForeignKey(lc => lc.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
