using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCategoryByIdCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCategoryByIdCommand, Response<int>>
    {
        private readonly ICourseRepositoryAsync _courseRepository;

        public DeleteCourseCommandHandler(ICourseRepositoryAsync courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<Response<int>> Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            var course=await _courseRepository.GetByIdAsync(request.Id);
            if (course == null) throw new ApiException($"Course Not Found.");
            await _courseRepository.DeleteAsync(course);
            return new Response<int>(course.Id);
        }
    }
}
