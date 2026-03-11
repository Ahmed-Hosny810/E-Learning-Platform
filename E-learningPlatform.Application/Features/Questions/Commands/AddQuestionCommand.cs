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

namespace E_learningPlatform.Application.Features.Questions.Commands
{
    public class AddQuestionCommand : IRequest<Response<int>>
    {
        public int QuizId { get; set; }
        public string QuestionText { get; set; } = null!;
        public string QuestionType { get; set; } = null!;
        public int Points { get; set; }
        public int OrderIndex { get; set; }
        public string Explanation { get; set; } = null!;
        public List<QuestionOptionDto> Options { get; set; } = new();
    }

    public class AddQuestionCommandHandler : IRequestHandler<AddQuestionCommand, Response<int>>
    {
        private readonly IQuestionRepositoryAsync _questionRepository;
        private readonly IMapper _mapper;

        public AddQuestionCommandHandler(IQuestionRepositoryAsync questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = _mapper.Map<Question>(request);
            await _questionRepository.AddAsync(question);
            return new Response<int>(question.Id);
        }
    }
}
