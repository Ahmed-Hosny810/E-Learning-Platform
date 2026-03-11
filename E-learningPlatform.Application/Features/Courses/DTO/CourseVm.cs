using E_learningPlatform.Application.Features.Categories.DTO;
using E_learningPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Courses.DTO
{
    public class CourseVm
    {
        public int Id { get; set; }

        public string TeacherId { get; set; }      

        public string Title { get; set; }
        public string Slug { get; set; }

        public string Description { get; set; }
        public string? ThumbnailUrl { get; set; }

        public CourseLevel Level { get; set; }               
        public string Language { get; set; }

        public decimal PriceUSD { get; set; }

        public string? Requirements { get; set; }
        public string? WhatYouWillLearn { get; set; }

        public bool IsPublished { get; set; }

        public int EnrollmentCount { get; set; }
        public decimal AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public int DurationMinutes { get; set; }

        public List<CategorySimpleDto> Categories { get; set; } = new();
    }
}
