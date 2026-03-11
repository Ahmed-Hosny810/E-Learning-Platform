using E_learningPlatform.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.QuizAttempts.Queries.GetAll
{
    public class GetStudentQuizHistoryParameter:RequestParameter<QuizHistoryOrderKey>
    {       
    }

    public enum QuizHistoryOrderKey
    {
        CompletedAt,
        Score,
        Percentage,
        QuizTitle
    }
}
