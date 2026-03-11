using E_learningPlatform.Application.Parameters;
using E_learningPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.LessonContents.Queries.GetAllQuery
{
    public class GetAllLessonContentParameter:RequestParameter<LessonContentOrderKey>
    {
        public LessonContentFilter? Filter { get; set; }
        public LessonContentIncludes? Includes { get; set; }
    }
    public class LessonContentFilter
    {
        public int? LessonId { get; set; }
        public LessonContentType? ContentType { get; set; }
    }
    public class LessonContentIncludes
    {
        public bool Lesson { get; set; }
    }
    public enum LessonContentOrderKey
    {
        Id,
        DisplayOrder,
        CreatedAt,
        UpdatedAt
    }
}
