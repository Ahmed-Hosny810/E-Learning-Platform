using E_learningPlatform.Application.Parameters;
using E_learningPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Courses.Queries.GetAllCourses
{
    public class GetAllCoursesParameter:RequestParameter<CourseOrderKey>
    {
        public CourseFilter Filter { get; set; } = new();
        public CourseIncludes Includes { get; set; } = new();
    }
    public class CourseFilter
    {
        public int? Id { get; set; }
        public string? TeacherId { get; set; }
        public string? Title { get; set; }

        public CourseLevel? Level { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public string? Language { get; set; }
        public List<int>? CategoryIds { get; set; }

        public DateTime? PublishedAfter { get; set; }
        public DateTime? PublishedBefore { get; set; }

        public decimal? MinRating { get; set; }
    }
    public class CourseIncludes
    {
        public bool Categories { get; set; }
        public bool Sections { get; set; }
        public bool Reviews { get; set; }

    }
    public enum CourseOrderKey
    {
        Title,
        Price,
        Currency,
        Language,
        Level,
        PublishedAt,
        AverageRating,
        EnrollmentCount
    }
}
