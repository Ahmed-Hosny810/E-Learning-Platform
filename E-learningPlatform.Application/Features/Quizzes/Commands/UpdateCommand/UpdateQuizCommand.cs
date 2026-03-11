using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Quizzes.Commands.UpdateCommand
{
    public class UpdateQuizCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TimeLimitMinutes { get; set; }
        public int PassingScore { get; set; }
        public int MaxAttempts { get; set; }
        public bool ShowCorrectAnswers { get; set; }
        public bool IsRequired { get; set; }
    }
    public class UpdateQuizCommandHandler : IRequestHandler<UpdateQuizCommand, Response<int>>
    {
        private readonly IQuizRepositoryAsync _quizRepository;

        public UpdateQuizCommandHandler(IQuizRepositoryAsync quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<Response<int>> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
        {
            var quiz = await _quizRepository.GetByIdAsync(request.Id);

            if (quiz == null) throw new ApiException($"Quiz Not Found.");
            quiz.Title = request.Title;
            quiz.Description = request.Description;
            quiz.TimeLimitMinutes = request.TimeLimitMinutes;
            quiz.PassingScore = request.PassingScore;
            quiz.MaxAttempts = request.MaxAttempts;
            quiz.ShowCorrectAnswers = request.ShowCorrectAnswers;
            quiz.IsRequired = request.IsRequired;

            await _quizRepository.UpdateAsync(quiz);
            return new Response<int>(quiz.Id);
        }
    }
}
