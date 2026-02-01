using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.CourseCategories.Commands.UpdateCourseCategory
{
    public class UpdateCourseCategoryCommand: IRequest<Response<bool>>
    {
        public int CourseId { get; set; }

        public int OldCategoryId { get; set; }
        public int NewCategoryId { get; set; }
    }
    public class UpdateCourseCategoryCommandHandler
    : IRequestHandler<UpdateCourseCategoryCommand, Response<bool>>
    {
        private readonly ICourseCategoryRepositoryAsync _repository;

        public UpdateCourseCategoryCommandHandler(
            ICourseCategoryRepositoryAsync courseCategoryRepository)
        {
            _repository = courseCategoryRepository;
        }

        public async Task<Response<bool>> Handle(
            UpdateCourseCategoryCommand request,
            CancellationToken cancellationToken)
        {

            var existing = await _repository.GetByIdsAsync(
                request.CourseId,
                request.OldCategoryId);

            if (existing == null)
            {
                 throw new ApiException("Course category not found");
            }

            // 2️⃣ Remove old relation
            await _repository.DeleteAsync(existing);

            // 3️⃣ Create new relation
            var newRelation = new CourseCategory
            {
                CourseId = request.CourseId,
                CategoryId = request.NewCategoryId,
                AssignedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(newRelation);

            return new Response<bool>(true);
        }
    }
}
