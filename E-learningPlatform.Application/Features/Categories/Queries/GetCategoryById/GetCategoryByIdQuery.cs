using AutoMapper;
using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Features.Categories.DTO;
using E_learningPlatform.Application.Features.Categories.Queries.GetAllCategories;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery:IRequest<Response<CategoryVm>>
    {
       public int Id { get; set; }
        public CategoryIncludes Includes { get; set; }
    }
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery,Response<CategoryVm>>
        {
        private readonly ICategoryRepositoryAsync _categoryRepository;
        private readonly IMapper _mapper;
        public GetCategoryByIdQueryHandler(ICategoryRepositoryAsync categoryRepository,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Response<CategoryVm>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(request.Id, request.Includes);
             
            if (category == null)
            {
                throw new ApiException($"Category Not Found.");
            }
            var categoryVm = _mapper.Map<CategoryVm>(category);
            return new Response<CategoryVm>(categoryVm);
        }
    }
}
