using AutoMapper;
using E_learningPlatform.Application.Features.Lessons.DTO;
using E_learningPlatform.Application.Features.Lessons.Queries.GetAllQuery;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Lessons.Queries.GetAllQuery
{
    public class GetAllLessonsQuery:IRequest<PagedResponse<IEnumerable<LessonVm>>>
    {
        public GetAllLessonsQueryParameter Parameter { get; set; }
       
    }
    public class GetAllLessonsQueryHandler :IRequestHandler<GetAllLessonsQuery, PagedResponse<IEnumerable<LessonVm>>>
    {
        private readonly ILessonRepositoryAsync _lessonRepository;
        private readonly IMapper _mapper;

        public GetAllLessonsQueryHandler(ILessonRepositoryAsync lessonRepository,IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<LessonVm>>> Handle(GetAllLessonsQuery request, CancellationToken cancellationToken)
        {
            var pagedLessons = await _lessonRepository.GetLessonsPagedResponseAsync(request.Parameter.Filter, request.Parameter.Includes,
                        request.Parameter.OrderKey,request.Parameter.OrderDescending, request.Parameter.PageNumber, request.Parameter.PageSize);
            var lessonVms = _mapper.Map<IEnumerable<LessonVm>>(pagedLessons.Data);
             return new PagedResponse<IEnumerable<LessonVm>>(lessonVms, request.Parameter.PageNumber, request.Parameter.PageSize, pagedLessons.TotalCount);
        }
    }
}
