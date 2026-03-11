using E_learningPlatform.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class QuizAttempt : BaseEntity
    {
        public int QuizId { get; set; }
        public int EnrollmentId { get; set; }

        public string StudentId { get; set; } = null!;

        // Timing data
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }

        // Scoring
        public int Score { get; set; }
        public int TotalPoints { get; set; }
        public decimal Percentage { get; set; }
        public bool IsPassed { get; set; }

        // Tracking retries
        public int AttemptNumber { get; set; }

        // Status: InProgress, Completed, Abandoned
        public string Status { get; set; } = string.Empty;

        public Quiz Quiz { get; set; } = null!;
        public Enrollment Enrollment { get; set; } = null!;
        public ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
    }
}
