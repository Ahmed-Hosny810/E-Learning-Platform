using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Lessons.DTO
{
    public class LessonVm
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public string Title { get; set; }
        public int DisplayOrder { get; set; }
        public int? DurationMinutes { get; set; }
        public bool IsFree { get; set; }
        public bool IsPublished { get; set; }
    }
}
