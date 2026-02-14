using E_learningPlatform.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Sections.Queries.GetAllQuery
{
    public class GetAllSectionsQueryParameter:RequestParameter<SectionOrderKey>
    {
        public SectionFilter? Filter { get; set; } 
        public SectionIncludes? Includes { get; set; }
    }    
    
    public class SectionFilter
    {
        public int? Id { get; set; }
        public int? CourseId { get; set; }
        public string? Title { get; set; }
        public bool? IsPublished { get; set; }
    }
    public class SectionIncludes
    {
        public bool Lessons { get; set; }
        public bool Course { get; set; }
    }
    public enum SectionOrderKey
    {
        Title,
        DisplayOrder,
        DurationMinutes,
        IsPublished,
        CreatedAt,
        UpdatedAt
    }
}
