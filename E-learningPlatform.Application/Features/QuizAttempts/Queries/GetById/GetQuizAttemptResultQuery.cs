using AutoMapper;
using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Features.QuizAttempts.DTO;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.QuizAttempts.Queries.GetById
{
    public class GetQuizAttemptResultQuery : IRequest<Response<QuizResultDto>>
    {
        public int AttemptId { get; set; }
    }

    public class GetQuizAttemptResultHandler : IRequestHandler<GetQuizAttemptResultQuery, Response<QuizResultDto>>
    {
        private readonly IQuizAttemptRepositoryAsync _attemptRepository;
        private readonly IMapper _mapper;

        public GetQuizAttemptResultHandler(IQuizAttemptRepositoryAsync attemptRepository, IMapper mapper)
        {
            _attemptRepository = attemptRepository;
            _mapper = mapper;
        }

        public async Task<Response<QuizResultDto>> Handle(GetQuizAttemptResultQuery request, CancellationToken cancellationToken)
        {
            // 1. Fetch attempt with all related answers, questions, and options
            var attempt = await _attemptRepository.GetAttemptWithQuizDataAsync(request.AttemptId);

            if (attempt == null) throw new ApiException("Attempt not found.");

            //if (attempt.StudentId != _userService.UserId) throw new ApiException("Unauthorized access.");

            var result = _mapper.Map<QuizResultDto>(attempt);
            return new Response<QuizResultDto>(result);
        }
    }
}
