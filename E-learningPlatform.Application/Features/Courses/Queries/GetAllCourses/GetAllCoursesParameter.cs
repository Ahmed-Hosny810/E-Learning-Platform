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
        public CourseIncludes Includes { get; set; } = CourseIncludes.None;
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
    [Flags]
    public enum CourseIncludes
    {
        None = 0,
        Teacher = 1,
        Categories = 2,
        Sections = 4,
        Reviews = 8
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
