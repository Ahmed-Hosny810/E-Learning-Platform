using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Domain.Models
{
    public class SectionProgress
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public int SectionId { get; set; }

        public int CompletedLessonsCount { get; set; }
        public int TotalLessonsCount { get; set; }

        public decimal CompletionPercentage { get; set; }

        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        // Navigation
        public  Section? Section { get; set; }
    }
}
