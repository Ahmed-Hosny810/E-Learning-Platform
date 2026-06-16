using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Quizzes.DTO
{
    public class StudentQuizDto : QuizDto<StudentQuestionDto, StudentOptionDto>
    {
    }
    public class StudentQuestionDto : QuestionDto<StudentOptionDto>
    {
    }
    public class StudentOptionDto : OptionDto
    {
    }
}