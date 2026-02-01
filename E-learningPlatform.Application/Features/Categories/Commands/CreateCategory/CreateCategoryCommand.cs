using AutoMapper;
using E_learningPlatform.Application.Features.Courses.DTO;
using E_learningPlatform.Application.Helpers;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand:IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }

        public int DisplayOrder { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<int>>
    {
        private readonly ICategoryRepositoryAsync _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepositoryAsync categoryRepository,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category=_mapper.Map<Category>(request);
            category.Slug = SlugHelper.Generate(request.Name);
            category.CreatedAt = DateTime.UtcNow;
            category.UpdatedAt = DateTime.UtcNow;
            await _categoryRepository.AddAsync(category);

            return new Response<int>(category.Id);
        }
    }
}
