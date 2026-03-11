using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.QuizAttempts.Commands.CreateCommand
{
    public class StartQuizAttemptCommand : IRequest<Response<int>>
    {
        public int QuizId { get; set; }
        public int EnrollmentId { get; set; }
        public string StudentId { get; set; } = null!; // to be replaced with the id extracted from the user service 
    }
    public class StartQuizAttemptHandler : IRequestHandler<StartQuizAttemptCommand, Response<int>>
    {
        private readonly IQuizRepositoryAsync _quizRepository;
        private readonly IQuizAttemptRepositoryAsync _attemptRepository;

        public StartQuizAttemptHandler(IQuizRepositoryAsync quizRepository, IQuizAttemptRepositoryAsync attemptRepository)
        {
            _quizRepository = quizRepository;
            _attemptRepository = attemptRepository;
        }

        public async Task<Response<int>> Handle(StartQuizAttemptCommand request, CancellationToken cancellationToken)
        {
            // 1. Get Quiz Config
            var quiz = await _quizRepository.GetByIdAsync(request.QuizId);
            if (quiz == null) throw new ApiException("Quiz not found");

            // 2. Check for existing "InProgress" attempt

            var activeAttempt = await _attemptRepository.GetActiveAttemptAsync(request.StudentId, request.QuizId);
            if (activeAttempt != null)
            {
                return new Response<int>(activeAttempt.Id);
            }

            // 3. Check Max Attempts
            int count = await _attemptRepository.GetAttemptCountAsync(request.StudentId, request.QuizId);
            if (count >= quiz.MaxAttempts)
            {
                throw new ApiException("You have reached the maximum number of attempts for this quiz.");
            }

            // 4. Create New Attempt
            var newAttempt = new QuizAttempt
            {
                QuizId = request.QuizId,
                EnrollmentId = request.EnrollmentId,
                StudentId = request.StudentId,
                StartedAt = DateTime.UtcNow,
                Status = "InProgress", 
                AttemptNumber = count + 1
            };

            await _attemptRepository.AddAsync(newAttempt);
            return new Response<int>(newAttempt.Id);
        }
    }
}
