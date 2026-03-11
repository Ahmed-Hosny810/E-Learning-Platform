using AutoMapper;
using E_learningPlatform.Application.Features.QuizAttempts.DTO;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.QuizAttempts.Queries.GetAll
{
    public class GetStudentQuizHistoryQuery : IRequest<PagedResponse<IEnumerable<QuizHistoryDto>>>
    {
        public GetStudentQuizHistoryParameter Parameter { get; set; }
    }

    public class GetStudentQuizHistoryHandler : IRequestHandler<GetStudentQuizHistoryQuery, PagedResponse<IEnumerable<QuizHistoryDto>>>
    {
        private readonly IQuizAttemptRepositoryAsync _attemptRepository;
        //private readonly IAuthenticatedUserService _userService;
        private readonly IMapper _mapper;

        public GetStudentQuizHistoryHandler(IQuizAttemptRepositoryAsync attemptRepository, IMapper mapper)
        {
            _attemptRepository = attemptRepository;
            //_userService = userService;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<QuizHistoryDto>>> Handle(GetStudentQuizHistoryQuery request, CancellationToken cancellationToken)
        {
            var studentId = "current-student-id";

            var attemptsPaged = await _attemptRepository.GetAttemptsByStudentIdAsync(
                studentId,
                request.Parameter.PageNumber,
                request.Parameter.PageSize);

            var historyDtoList = attemptsPaged.Data.Select(attempt => new QuizHistoryDto
            {
                AttemptId = attempt.Id,
                QuizTitle = attempt.Quiz?.Title ?? "Unknown Quiz", 
                Score = attempt.Score,
                TotalPoints = attempt.TotalPoints,
                Percentage = attempt.Percentage,
                Status = attempt.Status, 
                IsPassed = attempt.IsPassed,
                CompletedAt = attempt.CompletedAt ?? attempt.StartedAt 
            }).ToList();

            return new PagedResponse<IEnumerable<QuizHistoryDto>>(
                historyDtoList,
                request.Parameter.PageNumber,
                request.Parameter.PageSize,
                attemptsPaged.TotalCount);
        }
    }
}
