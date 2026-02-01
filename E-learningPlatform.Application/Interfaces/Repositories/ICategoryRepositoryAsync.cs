using E_learningPlatform.Application.Features.Categories.Queries.GetAllCategories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Interfaces.Repositories
{
    public interface ICategoryRepositoryAsync :IGenericRepositoryAsync<Category,int>
    {
        Task<PagedResponse<IEnumerable<Category>>> GetCategoriesPagedResponseAsync(CategoryFilter filter, CategoryIncludes includes,
            CategoryOrderKey orderKey, bool orderDescending, int currentPage, int pageSize);

        Task<Category> GetCategoryByIdAsync(int id, CategoryIncludes includes);
    }
}
