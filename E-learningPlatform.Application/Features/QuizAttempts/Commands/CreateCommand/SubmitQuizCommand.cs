using AutoMapper;
using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Features.QuizAttempts.DTO;
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

        public SubmitQuizCommandHandler(IQuizRepositoryAsync quizRepository, IQuizAttemptRepositoryAsync attemptRepository,IMapper mapper)
        {
            _quizRepository = quizRepository;
            _attemptRepository = attemptRepository;
            _mapper = mapper;
        }
        public async Task<Response<QuizResultDto>> Handle(SubmitQuizCommand request, CancellationToken cancellationToken)
        {
            var attempt = await _attemptRepository.GetAttemptWithQuizDataAsync(request.QuizAttemptId);

            if (attempt == null) throw new ApiException("Attempt not found.");

            //if (attempt.StudentId != _userService.UserId) throw new ApiException("Unauthorized attempt access.");

            if (attempt.Status != "InProgress") throw new ApiException("Quiz has already been submitted or is closed.");

            var elapsed = DateTime.UtcNow - attempt.StartedAt;
            if (elapsed.TotalMinutes > (attempt.Quiz.TimeLimitMinutes + 2)) // +2 min for delay period
            {
               attempt.Status = "TimedOut";
            }

            int totalPointsEarned = 0;
            int totalPossiblePoints = 0;

            foreach (var question in attempt.Quiz.Questions)
            {
                totalPossiblePoints += question.Points;
                var studentAnswer = request.Answers.FirstOrDefault(a => a.QuestionId == question.Id);

                var userAnswer = new UserAnswer
                {
                    QuizAttemptId = attempt.Id,
                    QuestionId = question.Id,
                    SelectedOptionId = studentAnswer?.SelectedOptionId,
                    IsCorrect = false,
                    PointsEarned = 0,
                    AnsweredAt = DateTime.UtcNow
                };

                // Check if correct
                var correctOption = question.QuestionOptions.FirstOrDefault(x => x.IsCorrect);
                if (studentAnswer != null && studentAnswer.SelectedOptionId == correctOption?.Id)
                {
                    userAnswer.IsCorrect = true;
                    userAnswer.PointsEarned = question.Points;
                    totalPointsEarned += question.Points;
                }

                attempt.UserAnswers.Add(userAnswer);
            }
            attempt.Score = totalPointsEarned;
            attempt.TotalPoints = totalPossiblePoints;
            attempt.Percentage = totalPossiblePoints > 0
            ? Math.Round((decimal)totalPointsEarned / totalPossiblePoints * 100, 2): 0;
            attempt.IsPassed = attempt.Percentage >= attempt.Quiz.PassingScore;
            attempt.Status = "Completed";
            attempt.CompletedAt = DateTime.UtcNow;

            await _attemptRepository.UpdateAsync(attempt);

            var resultDto = _mapper.Map<QuizResultDto>(attempt);

            return new Response<QuizResultDto>(resultDto);

        }
    }

}
