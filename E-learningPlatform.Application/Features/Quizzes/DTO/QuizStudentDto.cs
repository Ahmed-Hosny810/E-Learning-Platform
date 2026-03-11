using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Quizzes.DTO
{
    
    public class QuizStudentDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int TimeLimitMinutes { get; set; }
        public List<QuestionStudentDto> Questions { get; set; } = new();
    }

    public class QuestionStudentDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = null!;
        public string QuestionType { get; set; } = null!;
        public int Points { get; set; }
        public List<OptionStudentDto> Options { get; set; } = new();
    }

    public class OptionStudentDto
    {
        public int Id { get; set; }
        public string OptionText { get; set; } = null!;
    }

}
