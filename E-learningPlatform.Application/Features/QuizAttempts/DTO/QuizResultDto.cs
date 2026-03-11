using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.QuizAttempts.DTO
{
    public class QuizResultDto
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int TotalPoints { get; set; }
        public decimal Percentage { get; set; }
        public bool IsPassed { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CompletedAt { get; set; }
        public List<QuestionResultDto> Questions { get; set; } = new();
    }

    public class QuestionResultDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public int PointsEarned { get; set; }
        public string Explanation { get; set; } = null!;
        public int? SelectedOptionId { get; set; }
    }
}
