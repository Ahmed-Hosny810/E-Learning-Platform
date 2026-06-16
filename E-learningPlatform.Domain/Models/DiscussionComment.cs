using E_learningPlatform.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class DiscussionComment: BaseEntity
    {
        public int PostId { get; set; }
        public string UserId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public bool IsEdited { get; set; }
        public int? ParentCommentId { get; set; }
        public int LikeCount { get; set; }

        public  UserProfile UserProfile { get; set; } = null!;
        public  DiscussionPost Post { get; set; } = null!;
        public  DiscussionComment? ParentComment { get; set; }
        public  ICollection<DiscussionComment> Replies { get; set; } = new HashSet<DiscussionComment>();
    }
}
