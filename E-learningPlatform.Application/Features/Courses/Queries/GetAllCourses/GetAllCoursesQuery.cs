using AutoMapper;
using E_learningPlatform.Application.Features.Courses.DTO;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Courses.Queries.GetAllCourses
{
    public class GetAllCoursesQuery:IRequest<PagedResponse<IEnumerable<CourseVm>>>
    {
        public GetAllCoursesParameter Parameter { get; set; }
    }
    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, PagedResponse<IEnumerable<CourseVm>>>
    {
        private readonly ICourseRepositoryAsync _courseRepository;
        private readonly IMapper _mapper;
        public GetAllCoursesQueryHandler(ICourseRepositoryAsync courseRepository,IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<CourseVm>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var pagedCourses = await _courseRepository.GetCoursesPagedResponseAsync(request.Parameter.Filter,request.Parameter.Includes,
                                request.Parameter.OrderKey, request.Parameter.OrderDescending, request.Parameter.PageNumber, request.Parameter.PageSize);

            var courseVms = _mapper.Map<IEnumerable<CourseVm>>(pagedCourses.Data);
            return new PagedResponse<IEnumerable<CourseVm>>(
            courseVms,
            request.Parameter.PageNumber,
            request.Parameter.PageSize,
            pagedCourses.TotalCount);
        }
    }
    
}
