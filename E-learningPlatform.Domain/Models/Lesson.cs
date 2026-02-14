using E_learningPlatform.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class Lesson:BaseEntity
    {
        public Lesson()
        {
            LessonContents = new HashSet<LessonContent>();
        }
        public int SectionId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public int? DurationMinutes { get; set; }
        public bool IsFree { get; set; }
        public bool IsPublished { get; set; }
        public Section? Section { get; set; }
        public  ICollection<LessonContent> LessonContents { get; set; }
    }
}
