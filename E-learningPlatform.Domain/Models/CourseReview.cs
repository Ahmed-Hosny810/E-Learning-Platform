using E_learningPlatform.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class CourseReview : BaseEntity
    {
        public int CourseId { get; set; }
        public string UserId { get; set; } = null!;
        public int? EnrollmentId { get; set; }
        public int Rating { get; set; }  // 1-5 stars
        public string? ReviewText { get; set; }
        public bool IsApproved { get; set; }
        public string? InstructorResponse { get; set; }
        public DateTime? RespondedAt { get; set; }

        // Navigation properties
        public Course Course { get; set; } = null!;
        public UserProfile UserProfile { get; set; } = null!;
        public Enrollment Enrollment { get; set; } = null!;
    }
}
