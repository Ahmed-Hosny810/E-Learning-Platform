using E_learningPlatform.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class Section : BaseEntity
    {
        public Section()
        {
            Lessons=new HashSet<Lesson>();
        }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public int? DurationMinutes { get; set; }
        public bool IsPublished { get; set; }
        public  Course? Course { get; set; }
        public  ICollection<Lesson?> Lessons { get; set; }
    }
}
