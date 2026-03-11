using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Quizzes.DTO
{
    public class QuizEditorDto : QuizStudentDto
    {
        public int PassingScore { get; set; }
        public int MaxAttempts { get; set; }
        public bool ShowCorrectAnswers { get; set; }
        public new List<QuestionEditorDto> Questions { get; set; } = new();
    }

    public class QuestionEditorDto : QuestionStudentDto
    {
        public string Explanation { get; set; } = null!; 
        public new List<OptionEditorDto> Options { get; set; } = new();
    }

    public class OptionEditorDto : OptionStudentDto
    {
        public bool IsCorrect { get; set; } 
    }
}
