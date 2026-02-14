using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }

        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; } 
    }
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Response<int>>
    {
        private readonly ICategoryRepositoryAsync _categoryRepository;

        public UpdateCategoryCommandHandler(ICategoryRepositoryAsync categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Response<int>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category==null)
            {
                throw new ApiException($"Category Not Found.");
            }
                category.Name = request.Name;
                category.Description = request.Description;
                category.ParentCategoryId = request.ParentCategoryId;
                category.DisplayOrder = request.DisplayOrder;
                category.IsActive = request.IsActive;
                await _categoryRepository.UpdateAsync(category);
                return new Response<int>(category.Id);
            
        }
    }
}
