using E_learningPlatform.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class DiscussionPost:BaseEntity
    {
        public int CourseId { get; set; }
        public int? LessonId { get; set; }
        public string UserId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public bool IsPinned { get; set; }
        public bool IsResolved { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }

        public Course Course { get; set; } = null!;
        public  Lesson? Lesson { get; set; } 
        public  UserProfile UserProfile { get; set; } = null!;
        public  ICollection<DiscussionComment> Comments { get; set; } = new HashSet<DiscussionComment>();

    }
}
