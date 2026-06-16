using AutoMapper;
using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Features.QuizAttempts.DTO;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Interfaces.Services;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Constants;
using E_learningPlatform.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.QuizAttempts.Commands.SubmitCommand
{
    public class SubmitQuizCommand: IRequest<Response<QuizResultDto>>
    {
        public int QuizAttemptId { get; set; }
        public List<QuestionAnswerDto> Answers { get; set; } = new();
    }
    public class SubmitQuizCommandHandler : IRequestHandler<SubmitQuizCommand, Response<QuizResultDto>>
    {
        private readonly IQuizRepositoryAsync _quizRepository;
        private readonly IQuizAttemptRepositoryAsync _attemptRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        public SubmitQuizCommandHandler(IQuizRepositoryAsync quizRepository, IQuizAttemptRepositoryAsync attemptRepository,IMapper mapper,
            INotificationService notificationService,IUserService userService)
        {
            _quizRepository = quizRepository;
            _attemptRepository = attemptRepository;
            _mapper = mapper;
            _notificationService = notificationService;
            _userService = userService;
        }
        public async Task<Response<QuizResultDto>> Handle(SubmitQuizCommand request, CancellationToken cancellationToken)
        {
            var attempt = await _attemptRepository.GetAttemptWithQuizDataAsync(request.QuizAttemptId);

            if (attempt == null) throw new ApiException("Attempt not found.");

            if (attempt.StudentId != _userService.UserId) throw new ApiException("Unauthorized attempt access.");

            if (attempt.Status != AttemptStatus.InProgress) throw new ApiException("Quiz has already been submitted or is closed.");

            if (attempt.IsExpired(attempt.Quiz.TimeLimitMinutes))
            {
               attempt.Status = AttemptStatus.Expired;
                await _attemptRepository.UpdateAsync(attempt);
                await _notificationService.NotifyQuizExpiredAsync(
                       attempt.StudentId,
                       attempt.Id,
                       attempt.Quiz.Title);
                throw new ApiException("Time has expired. Your attempt has been closed.");
            }

            int totalPointsEarned = 0;
            int totalPossiblePoints = 0;

            foreach (var question in attempt.Quiz.Questions)
            {
                totalPossiblePoints += question.Points;
                var studentAnswer = request.Answers.FirstOrDefault(a => a.QuestionId == question.Id);
                var correctOption = question.QuestionOptions.FirstOrDefault(x => x.IsCorrect);

                var userAnswer = new UserAnswer
                {
                    QuizAttemptId = attempt.Id,
                    QuestionId = question.Id,
                    SelectedOptionId = studentAnswer?.SelectedOptionId,
                    IsCorrect = false,
                    PointsEarned = 0,
                    AnsweredAt = DateTime.UtcNow
                };
                attempt.UserAnswers.Add(userAnswer);
            }
            attempt.Score = totalPointsEarned;
            attempt.TotalPoints = totalPossiblePoints;
            attempt.IsPassed = attempt.Percentage >= attempt.Quiz.PassingScore;
            attempt.Status = AttemptStatus.Completed;
            attempt.CompletedAt = DateTime.UtcNow;

            await _attemptRepository.UpdateAsync(attempt);

            await _notificationService.NotifyQuizSubmittedAsync(
                      attempt.StudentId,
                      attempt.Id,
                      attempt.Quiz.Title,
                      attempt.Score,
                      attempt.TotalPoints,
                      attempt.IsPassed);

            
            return new Response<QuizResultDto>(_mapper.Map<QuizResultDto>(attempt));

        }
    }

}
