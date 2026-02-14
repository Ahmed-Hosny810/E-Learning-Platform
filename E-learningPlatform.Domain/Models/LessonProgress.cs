using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class LessonProgress
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public int LessonId { get; set; }

        public bool IsCompleted { get; set; }
        public decimal CompletionPercentage { get; set; }

        public int? LastPositionSeconds { get; set; }
        public int TimeSpentSeconds { get; set; }

        public DateTime LastAccessedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        // Navigation
        public  Lesson? Lesson { get; set; }
    }
}
