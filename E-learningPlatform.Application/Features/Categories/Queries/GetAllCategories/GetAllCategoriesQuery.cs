using AutoMapper;
using E_learningPlatform.Application.Features.Categories.DTO;
using E_learningPlatform.Application.Features.Courses.DTO;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery:IRequest<PagedResponse<IEnumerable<CategoryVm>>>
    {
        public GetAllCategoriesParameters Parameter { get; set; }=new();
    }

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery,PagedResponse<IEnumerable<CategoryVm>>>
    {
        private readonly ICategoryRepositoryAsync _categoryRepository;
        private readonly IMapper _mapper;
        public GetAllCategoriesQueryHandler(ICategoryRepositoryAsync categoryRepository,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<CategoryVm>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var pagedCourses = await _categoryRepository.GetCategoriesPagedResponseAsync(request.Parameter.Filter, request.Parameter.Includes,
                                 request.Parameter.OrderKey, request.Parameter.OrderDescending, request.Parameter.PageNumber, request.Parameter.PageSize);

            var categoriesVms = _mapper.Map<IEnumerable<CategoryVm>>(pagedCourses.Data);
            return new PagedResponse<IEnumerable<CategoryVm>>(
            categoriesVms,
            request.Parameter.PageNumber,
            request.Parameter.PageSize,
            pagedCourses.TotalCount);
        }
    }
}
