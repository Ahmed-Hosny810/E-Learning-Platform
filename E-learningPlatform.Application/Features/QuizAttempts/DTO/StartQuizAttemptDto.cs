using E_learningPlatform.Application.Features.Quizzes.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.QuizAttempts.DTO
{
    public class StartQuizAttemptDto
    {
        public int AttemptId { get; set; }
        public StudentQuizDto Quiz { get; set; } = null!;
    }
}
