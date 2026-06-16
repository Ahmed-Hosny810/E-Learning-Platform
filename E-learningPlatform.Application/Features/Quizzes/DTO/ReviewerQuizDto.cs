namespace E_learningPlatform.Application.Features.Quizzes.DTO
{
    public class ReviewerQuizDto : QuizDto<ReviewerQuestionDto, ReviewerOptionDto>
    {
        public int PassingScore { get; set; }
        public int MaxAttempts { get; set; }
        public bool ShowCorrectAnswers { get; set; }
    }

    public class ReviewerQuestionDto : QuestionDto<ReviewerOptionDto>
    {
        public string? Explanation { get; set; }
    }

    public class ReviewerOptionDto : OptionDto
    {
        public bool IsCorrect { get; set; } 
    }
}
