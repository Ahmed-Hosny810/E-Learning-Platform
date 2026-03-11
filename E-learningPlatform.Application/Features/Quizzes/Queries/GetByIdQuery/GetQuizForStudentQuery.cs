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
    public class GetQuizForStudentQuery : IRequest<Response<QuizStudentDto>>
    {
        public int QuizId { get; set; }
    }

    public class GetQuizForStudentHandler : IRequestHandler<GetQuizForStudentQuery, Response<QuizStudentDto>>
    {
        private readonly IQuizRepositoryAsync _quizRepository;
        private readonly IMapper _mapper;

        public GetQuizForStudentHandler(IQuizRepositoryAsync quizRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }

        public async Task<Response<QuizStudentDto>> Handle(GetQuizForStudentQuery request, CancellationToken cancellationToken)
        {
            var quiz = await _quizRepository.GetQuizWithQuestionsAsync(request.QuizId);
            if (quiz == null) throw new Exception("Quiz not found");

            var quizDto = _mapper.Map<QuizStudentDto>(quiz);

            // Logic: Shuffle if enabled
            if (quiz.ShuffleQuestions)
            {
                quizDto.Questions = quizDto.Questions.OrderBy(x => Guid.NewGuid()).ToList();
            }

            if (quiz.ShuffleOptions)
            {
                foreach (var q in quizDto.Questions)
                {
                    q.Options = q.Options.OrderBy(x => Guid.NewGuid()).ToList();
                }
            }

            return new Response<QuizStudentDto>(quizDto);
        }
    }

}
