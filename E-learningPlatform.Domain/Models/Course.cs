using E_learningPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string TeacherId { get; set; } // External Auth ID
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string? ThumbnailUrl { get; set; }
        public CourseLevel Level { get; set; } // Enum: 0=Beginner, 1=Intermediate, 2=Advanced
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string Language { get; set; }
        public string? Requirements { get; set; }
        public string? WhatYouWillLearn { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Cached fields
        public int EnrollmentCount { get; set; }
        public decimal AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public int DurationMinutes { get; set; }

        public bool IsActive { get; set; }
        public virtual ICollection<CourseCategory> CourseCategories { get; set; } = new HashSet<CourseCategory>();
    }
}
