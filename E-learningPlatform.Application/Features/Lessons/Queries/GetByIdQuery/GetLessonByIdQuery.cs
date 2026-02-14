using AutoMapper;
using E_learningPlatform.Application.Exceptions;
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

namespace E_learningPlatform.Application.Features.Lessons.Queries.GetByIdQuery
{
    public class GetLessonByIdQuery:IRequest<Response<LessonDetailedVm>>
    {
        public int Id { get; set; }
        public LessonIncludes Includes { get; set; }
    }
    public class GetLessonByIdQueryHandler : IRequestHandler<GetLessonByIdQuery, Response<LessonDetailedVm>>
    {
        private readonly ILessonRepositoryAsync _lessonRepository;
        private readonly IMapper _mapper;

        public GetLessonByIdQueryHandler(ILessonRepositoryAsync lessonRepository,IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }
        public async Task<Response<LessonDetailedVm>> Handle(GetLessonByIdQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonRepository.GetLessonByIdAsync(request.Id, request.Includes);    
            if(lesson==null) throw new ApiException("Lesson not found");
            var lessonVm=_mapper.Map<LessonDetailedVm>(lesson);
            return new Response<LessonDetailedVm>(lessonVm);
        }
    }
}
