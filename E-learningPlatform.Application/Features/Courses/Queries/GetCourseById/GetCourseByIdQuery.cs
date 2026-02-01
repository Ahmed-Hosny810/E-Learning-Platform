using AutoMapper;
using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Features.Courses.DTO;
using E_learningPlatform.Application.Features.Courses.Queries.GetAllCourses;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Courses.Queries.GetCourseById
{
    public class GetCourseByIdQuery:IRequest<Response<CourseVm>>
    {
        public int Id { get; set; }
        public CourseIncludes Includes { get; set; }
    }
    public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, Response<CourseVm>>
    {
        private readonly ICourseRepositoryAsync _courseRepository;
        private readonly IMapper _mapper;

        public GetCourseByIdQueryHandler(ICourseRepositoryAsync courseRepository,IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        public async Task<Response<CourseVm>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetCourseByIdAsync(request.Id, request.Includes);
            if (course==null)
            {
                throw new ApiException($"Course Not Found.");
            }
            var courseVm=_mapper.Map<CourseVm>(course);
            return new Response<CourseVm>(courseVm);
        }
    }
}
