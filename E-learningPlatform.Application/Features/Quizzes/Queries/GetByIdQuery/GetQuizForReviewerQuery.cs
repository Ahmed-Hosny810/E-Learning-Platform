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
    public class GetQuizForReviewerQuery : IRequest<Response<ReviewerQuizDto>>
    {
        public int QuizId { get; set; }
    }

    public class GetQuizForReviewerHandler : IRequestHandler<GetQuizForReviewerQuery, Response<ReviewerQuizDto>>
    {
        private readonly IQuizRepositoryAsync _quizRepository;
        private readonly IMapper _mapper;

        public GetQuizForReviewerHandler(IQuizRepositoryAsync quizRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }

        public async Task<Response<ReviewerQuizDto>> Handle(GetQuizForReviewerQuery request, CancellationToken cancellationToken)
        {
            var quiz = await _quizRepository.GetQuizWithQuestionsAsync(request.QuizId);
            if (quiz == null) throw new Exception("Quiz not found");

            var quizDto = _mapper.Map<ReviewerQuizDto>(quiz);
            return new Response<ReviewerQuizDto>(quizDto);
        }
    }
}
