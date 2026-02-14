using E_learningPlatform.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Lessons.Queries.GetAllQuery
{
    public class GetAllLessonsQueryParameter:RequestParameter<LessonOrderKey>
    {
        public LessonFilter? Filter { get; set; }
        public LessonIncludes? Includes { get; set; }
    }
    public class LessonFilter
    {
        public int? Id { get; set; } 
        public int? SectionId { get; set; } 
        public int? DisplayOrder { get; set; } 
        public string? Title { get; set; } 
        public int? DurationMinutes { get; set; }
        public bool? IsFree { get; set; } 
        public bool? IsPublished { get; set; } 
    }
    public class LessonIncludes
    {
        public bool Section { get; set; }
        public bool LessonContent { get; set; }
    }
    public enum LessonOrderKey
    {
        Id, 
        Title, 
        DisplayOrder, 
        DurationMinutes,
        IsPublished,
        CreatedAt,
        UpdatedAt
    }
}
