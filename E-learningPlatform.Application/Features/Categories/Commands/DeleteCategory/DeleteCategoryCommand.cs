using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Response<int>>
    {
        private readonly ICategoryRepositoryAsync _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepositoryAsync categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Response<int>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null) throw new ApiException($"Category Not Found.");
            await _categoryRepository.DeleteAsync(category);
            return new Response<int>(category.Id);
        }
    }
}
