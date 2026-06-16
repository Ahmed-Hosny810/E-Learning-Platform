using E_learningPlatform.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class UserProfile:BaseEntity
    {
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
        public ICollection<DiscussionPost> DiscussionPosts { get; set; } = new HashSet<DiscussionPost>();
        public ICollection<DiscussionComment> DiscussionComments { get; set; } = new HashSet<DiscussionComment>();
        public ICollection<CourseReview> CourseReviews { get; set; } = new HashSet<CourseReview>();
        public ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();
        public ICollection<Message> SentMessages { get; set; } = new HashSet<Message>();
        public ICollection<Message> ReceivedMessages { get; set; } = new HashSet<Message>();

    }
}
