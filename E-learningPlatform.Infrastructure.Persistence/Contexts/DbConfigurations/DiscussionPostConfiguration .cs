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
    public class DiscussionPostConfiguration : IEntityTypeConfiguration<DiscussionPost>
    {
        public void Configure(EntityTypeBuilder<DiscussionPost> builder)
        {
            builder.ToTable("DiscussionPosts");

            builder.HasKey(dp => dp.Id);

            builder.Property(dp => dp.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(dp => dp.Content)
                .IsRequired()
                .HasMaxLength(4000);  

            builder.Property(dp => dp.IsPinned)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(dp => dp.IsResolved)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(dp => dp.ViewCount)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(dp => dp.CommentCount)
                .IsRequired()
                .HasDefaultValue(0);

            // Relationships
            builder.HasOne(dp => dp.Course)
                .WithMany(c => c.DiscussionPosts)
                .HasForeignKey(dp => dp.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(dp => dp.Lesson)
                .WithMany(l => l.DiscussionPosts)
                .HasForeignKey(dp => dp.LessonId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(dp => dp.UserProfile)
                   .WithMany(u => u.DiscussionPosts)
                   .HasForeignKey(dp => dp.UserId)
                   .HasPrincipalKey(u => u.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(dp => new { dp.CourseId, dp.CreatedAt })
                .HasDatabaseName("IX_DiscussionPosts_CourseId_CreatedAt");

            builder.HasIndex(dp => dp.LessonId)
                .HasDatabaseName("IX_DiscussionPosts_LessonId");

            builder.HasIndex(dp => dp.UserId)
                .HasDatabaseName("IX_DiscussionPosts_UserId");

            builder.HasIndex(dp => new { dp.IsPinned, dp.CreatedAt })
                .HasDatabaseName("IX_DiscussionPosts_IsPinned_CreatedAt");

            builder.HasIndex(dp => dp.IsResolved)
                .HasDatabaseName("IX_DiscussionPosts_IsResolved");
        }
    }
}
