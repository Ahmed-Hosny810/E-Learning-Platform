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
    public class CourseReviewConfiguration : IEntityTypeConfiguration<CourseReview>
    {
        public void Configure(EntityTypeBuilder<CourseReview> builder)
        {
            builder.HasKey(cr => cr.Id);

            // Properties

            builder.Property(cr => cr.UserId)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(cr => cr.Rating)
                .IsRequired();

            builder.Property(cr => cr.ReviewText)
                .HasMaxLength(1000);

            builder.Property(cr => cr.InstructorResponse)
                .HasMaxLength(1000);

            builder.Property(cr => cr.IsApproved)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(cr => cr.RespondedAt)
                .IsRequired(false);

            // Relationships
            builder.HasOne(cr => cr.Course)
                .WithMany(c => c.CourseReviews)
                .HasForeignKey(cr => cr.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cr => cr.UserProfile)
                    .WithMany(u => u.CourseReviews)
                    .HasForeignKey(cr => cr.UserId)
                    .HasPrincipalKey(u => u.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cr => cr.Enrollment)
                .WithOne(e => e.CourseReview)  
                .HasForeignKey<CourseReview>(cr => cr.EnrollmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(cr => cr.EnrollmentId)
                .IsUnique()  
                .HasDatabaseName("IX_CourseReviews_EnrollmentId");

            builder.HasIndex(cr => new { cr.CourseId, cr.IsApproved })
                .HasDatabaseName("IX_CourseReviews_CourseId_IsApproved");

            builder.HasIndex(cr => cr.UserId)
                .HasDatabaseName("IX_CourseReviews_UserId");

            builder.HasIndex(cr => cr.Rating)
                .HasDatabaseName("IX_CourseReviews_Rating");

            builder.HasIndex(cr => cr.CreatedAt)
                .HasDatabaseName("IX_CourseReviews_CreatedAt");

            builder.ToTable("CourseReviews", t=>t.HasCheckConstraint("CK_CourseReviews_Rating", "Rating >= 1 AND Rating <= 5"));

        }
    }
}
