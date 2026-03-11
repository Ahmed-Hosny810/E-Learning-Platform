using AutoMapper;
using E_learningPlatform.Application.Features.Quizzes.DTO;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Quizzes.Queries.GetByIdQuery
{
    public class GetQuizForEditorQuery : IRequest<Response<QuizEditorDto>>
    {
        public int QuizId { get; set; }
    }

    public class GetQuizForEditorHandler : IRequestHandler<GetQuizForEditorQuery, Response<QuizEditorDto>>
    {
        private readonly IQuizRepositoryAsync _quizRepository;
        private readonly IMapper _mapper;

        public GetQuizForEditorHandler(IQuizRepositoryAsync quizRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }

        public async Task<Response<QuizEditorDto>> Handle(GetQuizForEditorQuery request, CancellationToken cancellationToken)
        {
            var quiz = await _quizRepository.GetQuizWithQuestionsAsync(request.QuizId);
            if (quiz == null) throw new Exception("Quiz not found");

            var quizDto = _mapper.Map<QuizEditorDto>(quiz);
            return new Response<QuizEditorDto>(quizDto);
        }
    }
}
