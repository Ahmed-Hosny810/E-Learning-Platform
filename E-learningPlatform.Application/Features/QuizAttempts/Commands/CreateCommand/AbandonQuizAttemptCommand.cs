using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.QuizAttempts.Commands.CreateCommand
{
    public class AbandonQuizAttemptCommand:IRequest<Response<int>>
    {
        public int QuizAttemptId { get; set; }
    }
    public class AbandonQuizAttemptCommandHandler : IRequestHandler<AbandonQuizAttemptCommand, Response<int>>
    {
        private readonly IQuizAttemptRepositoryAsync _attemptRepository;
        public AbandonQuizAttemptCommandHandler(IQuizAttemptRepositoryAsync attemptRepository)
        {
            _attemptRepository = attemptRepository;
        }
        public async Task<Response<int>> Handle(AbandonQuizAttemptCommand request, CancellationToken cancellationToken)
        {
            var attempt = await _attemptRepository.GetByIdAsync(request.QuizAttemptId);
            if (attempt == null) throw new ApiException("Attempt not found.");
            //if (attempt.StudentId != _userService.UserId) throw new ApiException("Unauthorized attempt access.");
            if (attempt.Status != "InProgress") throw new ApiException("Quiz has already been submitted or is closed.");

            var timeElapsed = DateTime.UtcNow - attempt.StartedAt;

            if (timeElapsed.TotalMinutes >= attempt.Quiz.TimeLimitMinutes)
            {
                attempt.Status = "Abandoned";
                attempt.CompletedAt = DateTime.UtcNow;
            }
            else
            {
                // If the user manually clicks "Quit"
                attempt.Status = "Abandoned";
            }

            await _attemptRepository.UpdateAsync(attempt);
            return new Response<int>(attempt.Id);
        }
    }
}
