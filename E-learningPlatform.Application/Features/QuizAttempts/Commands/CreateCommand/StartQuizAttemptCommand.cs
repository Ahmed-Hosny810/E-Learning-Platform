using AutoMapper;
using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Features.Courses.DTO;
using E_learningPlatform.Application.Features.QuizAttempts.DTO;
using E_learningPlatform.Application.Features.Quizzes.DTO;
using E_learningPlatform.Application.Helpers;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Constants;
using E_learningPlatform.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.QuizAttempts.Commands.CreateCommand
{
    public class StartQuizAttemptCommand : IRequest<Response<StartQuizAttemptDto>>
    {
        public int QuizId { get; set; }
        public int CourseId { get; set; }
        public int EnrollmentId { get; set; }
        public string StudentId { get; set; } = null!; // to be replaced with actual user ID from auth context
    }
    public class StartQuizAttemptHandler : IRequestHandler<StartQuizAttemptCommand, Response<StartQuizAttemptDto>>
    {
        private readonly IQuizRepositoryAsync _quizRepository;
        private readonly IQuizAttemptRepositoryAsync _attemptRepository;
        private readonly IEnrollmentRepositoryAsync _enrollmentRepository;
        private readonly IMapper _mapper;

        public StartQuizAttemptHandler(IQuizRepositoryAsync quizRepository, IQuizAttemptRepositoryAsync attemptRepository,
            IEnrollmentRepositoryAsync enrollmentRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _attemptRepository = attemptRepository;
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }


        public async Task<Response<StartQuizAttemptDto>> Handle(StartQuizAttemptCommand request, CancellationToken cancellationToken)
        {

            var isUserEnrolled = await _enrollmentRepository.IsUserEnrolled(request.StudentId, request.CourseId);
            if (!isUserEnrolled) throw new ApiException("Invalid enrollment.");

            // 2. Check for Running Attempt
            var activeAttempt = await _attemptRepository.GetActiveAttemptAsync(request.StudentId, request.QuizId);


            var quiz = await _quizRepository.GetQuizWithQuestionsAsync(request.QuizId);
            if (quiz == null) throw new ApiException("Quiz not found.");

            if (activeAttempt != null)
            {
                if (activeAttempt.IsExpired(quiz.TimeLimitMinutes))
                {
                    activeAttempt.Status = AttemptStatus.Expired;
                    await _attemptRepository.UpdateAsync(activeAttempt);
                    throw new ApiException("Your previous attempt has expired.");
                }
                return BuildResponse(activeAttempt, quiz);

            }

            // 4. Check Max Attempts
            int count = await _attemptRepository.GetAttemptCountAsync(request.StudentId, request.QuizId);
            if (count >= quiz.MaxAttempts)
                throw new ApiException("Maximum attempts reached.");

            // 5. Create and Save
            var newAttempt = new QuizAttempt
            {
                QuizId = request.QuizId,
                EnrollmentId = request.EnrollmentId,
                StudentId = request.StudentId,
                StartedAt = DateTime.UtcNow,
                Status = AttemptStatus.InProgress,
                AttemptNumber = count + 1
            };

            await _attemptRepository.AddAsync(newAttempt);

            return BuildResponse(newAttempt, quiz);
        }


        private Response<StartQuizAttemptDto> BuildResponse(QuizAttempt attempt, Quiz quiz)
        {
            var quizDto = _mapper.Map<StudentQuizDto>(quiz);

            quizDto.StartedAt = attempt.StartedAt;
            quizDto.RemainingSeconds = attempt.GetRemainingSeconds(quiz.TimeLimitMinutes);

            if (quiz.ShuffleQuestions)
                quizDto.Questions = quizDto.Questions.OrderBy(_ => Guid.NewGuid()).ToList();

            if (quiz.ShuffleOptions)
                foreach (var question in quizDto.Questions)
                    question.Options = question.Options.OrderBy(_ => Guid.NewGuid()).ToList();

            return new Response<StartQuizAttemptDto>(new StartQuizAttemptDto
            {
                AttemptId = attempt.Id,
                Quiz = quizDto
            });
        }

    }
}
