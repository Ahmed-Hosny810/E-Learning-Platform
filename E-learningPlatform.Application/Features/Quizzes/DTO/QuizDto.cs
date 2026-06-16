using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Quizzes.DTO
{
    
    public class QuizDto<TQuestion, TOption>
        where TQuestion : QuestionDto<TOption>
        where TOption:OptionDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int TimeLimitMinutes { get; set; }
        public DateTime StartedAt { get; set; }
        public int RemainingSeconds { get; set; }
        public List<TQuestion> Questions { get; set; } = new();
    }

    public class QuestionDto<TOption> where TOption : OptionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = null!;
        public string QuestionType { get; set; } = null!;
        public int Points { get; set; }
        public List<TOption> Options { get; set; } = new();
    }

    public class OptionDto
    {
        public int Id { get; set; }
        public string OptionText { get; set; } = null!;
    }

}
