using AutoMapper;
using E_learningPlatform.Application.Features.Questions.DTO;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Quizzes.Commands.CreateCommand
{
    public class CreateQuizCommand:IRequest<Response<int>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int LessonId { get; set; }
        public int TimeLimitMinutes { get; set; }
        public int PassingScore { get; set; }
        public int MaxAttempts { get; set; }

        public bool ShowCorrectAnswers { get; set; }
        public bool ShuffleQuestions { get; set; }
        public bool ShuffleOptions { get; set; }
        public bool IsRequired { get; set; }
        public List<QuestionDto> Questions { get; set; } = new();
    }
    public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, Response<int>>
    {
        private readonly IQuizRepositoryAsync _quizRepository;
        private readonly IMapper _mapper;

        public CreateQuizCommandHandler(IQuizRepositoryAsync quizRepository,IMapper mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
           var quiz=_mapper.Map<Quiz>(request);
            await _quizRepository.AddAsync(quiz);
            return new Response<int>(quiz.Id);
        }
    }
}
