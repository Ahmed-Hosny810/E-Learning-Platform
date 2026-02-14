using AutoMapper;
using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Features.LessonContents.DTO;
using E_learningPlatform.Application.Features.LessonContents.Queries.GetAllQuery;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.LessonContents.Queries.GetByIdQuery
{
    public class GetLessonContentByIdQuery:IRequest<Response<LessonContentVm>>
    {
        public int Id { get; set; }
        public LessonContentIncludes? Includes { get; set; }
    }
    public class GetLessonContentByIdQueryHandler : IRequestHandler<GetLessonContentByIdQuery, Response<LessonContentVm>>
    {
        private readonly ILessonContentRepositoryAsync _repository;
        private readonly IMapper _mapper;

        public GetLessonContentByIdQueryHandler(ILessonContentRepositoryAsync repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<LessonContentVm>> Handle(GetLessonContentByIdQuery request, CancellationToken cancellationToken)
        {
            var lessonContent = await _repository.GetLessonContentByIdAsync(request.Id, request.Includes);
            if (lessonContent == null) throw new ApiException("Lesson Content not found ");

            var lessonContentVm = _mapper.Map<LessonContentVm>(lessonContent);

            return new Response<LessonContentVm>(lessonContentVm);
        }
    }   
}
