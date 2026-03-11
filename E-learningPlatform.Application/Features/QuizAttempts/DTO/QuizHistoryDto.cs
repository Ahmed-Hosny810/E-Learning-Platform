using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.QuizAttempts.DTO
{
    public class QuizHistoryDto
    {
        public int AttemptId { get; set; }
        public string QuizTitle { get; set; } = null!;
        public int Score { get; set; }
        public int TotalPoints { get; set; }
        public decimal Percentage { get; set; }
        public string Status { get; set; } = null!;
        public bool IsPassed { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
