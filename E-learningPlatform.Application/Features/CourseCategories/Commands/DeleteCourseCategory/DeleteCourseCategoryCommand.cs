using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.CourseCategories.Commands.DeleteCourseCategory
{
    public class DeleteCourseCategoryCommand : IRequest<Response<bool>>
    {
        public int CourseId { get; set; }
        public int CategoryId { get; set; }
    }
    public class DeleteCourseCategoryCommandHandler:IRequestHandler<DeleteCourseCategoryCommand, Response<bool>>
    {
        private readonly ICourseCategoryRepositoryAsync _repository;

        public DeleteCourseCategoryCommandHandler(
            ICourseCategoryRepositoryAsync repository)
        {
            _repository = repository;
        }

        public async Task<Response<bool>> Handle(
            DeleteCourseCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdsAsync(
                request.CourseId,
                request.CategoryId);

            if (entity == null)
            {
                throw new ApiException("Course category not found");
                
            }

            await _repository.DeleteAsync(entity);
            return new Response<bool>(true);
        }
    }
}
