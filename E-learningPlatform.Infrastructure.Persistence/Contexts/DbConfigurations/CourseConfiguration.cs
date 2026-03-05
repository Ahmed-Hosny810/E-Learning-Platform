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
    public class CourseConfiguration:IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.TeacherId)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(c => c.Slug)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasIndex(c => c.Slug)
                .IsUnique();

            builder.Property(c => c.Description)
                .HasMaxLength(5000);

            builder.Property(c => c.ThumbnailUrl)
                .HasMaxLength(500);

            builder.Property(c => c.Level)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(c => c.PriceUSD)
                .HasPrecision(18, 2);


            builder.Property(c => c.Language)
                .HasMaxLength(10);

            builder.Property(c => c.Requirements)
                .HasMaxLength(4000);

            builder.Property(c => c.WhatYouWillLearn)
                .HasMaxLength(4000);

            // Cached fields
            builder.Property(c => c.EnrollmentCount)
                .HasDefaultValue(0);

            builder.Property(c => c.AverageRating)
                .HasPrecision(3, 2)
                .HasDefaultValue(0);

            builder.Property(c => c.TotalReviews)
                .HasDefaultValue(0);

            builder.Property(c => c.DurationMinutes)
                .HasDefaultValue(0);

            builder.Property(c => c.IsActive)
                .HasDefaultValue(true);

            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.Property(c => c.UpdatedAt)
                .IsRequired();
        }
    }
}
