using AutoMapper;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.CourseCategories.Commands.CreateCourseCategory
{
    public class CreateCourseCategoryCommand:IRequest<Response<int>>
    {
        public int CourseId { get; set; }
        public int CategoryId { get; set; }
    }

    public class CreateCourseCategoryCommandHandler : IRequestHandler<CreateCourseCategoryCommand, Response<int>>
    {
        private readonly ICourseCategoryRepositoryAsync _courseCategoryRepository;

        public CreateCourseCategoryCommandHandler(ICourseCategoryRepositoryAsync courseCategoryRepository)
        {
            _courseCategoryRepository = courseCategoryRepository;
        }
        public async Task<Response<int>> Handle(CreateCourseCategoryCommand request, CancellationToken cancellationToken)
        {
            if (await _courseCategoryRepository.ExistsAsync(request.CourseId, request.CategoryId))
            {
                //throw new ApiException("Course already assigned to this category.");
            }

            var courseCategory = new CourseCategory
            {
                CourseId = request.CourseId,
                CategoryId = request.CategoryId,
                AssignedAt = DateTime.UtcNow
            };
            await _courseCategoryRepository.AddAsync(courseCategory);
            return new Response<int>(courseCategory.CourseId);
        }
    }
}
