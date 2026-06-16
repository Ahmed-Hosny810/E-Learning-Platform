using E_learningPlatform.Application.Features.QuizAttempts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_learningPlatform.Application.Interfaces.Hubs
{
    public interface IQuizClient
    {
        Task TimeWarning(int remainingSeconds);
        Task QuizExpired(int attemptId);
        Task QuizSubmitted(QuizResultDto result);
    }
}
