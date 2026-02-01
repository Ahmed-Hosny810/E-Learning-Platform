using E_learningPlatform.Application.Features.Categories.Queries.GetAllCategories;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using E_learningPlatform.Infrastructure.Persistence.Contexts;
using E_learningPlatform.Infrastructure.Persistence.QueryHelpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.Repositories
{
    public class CategoryRepositoryAsync
        : GenericRepositoryAsync<Category, int>,
          ICategoryRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepositoryAsync(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<PagedResponse<IEnumerable<Category>>> GetCategoriesPagedResponseAsync(
            CategoryFilter filter,
            CategoryIncludes includes,
            CategoryOrderKey orderKey,
            bool orderDescending,
            int currentPage,
            int pageSize)
        {
            currentPage = currentPage <= 0 ? 1 : currentPage;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var baseQuery = _context.Categories.AsQueryable();

            var helper = new CategoryQueryHelper(baseQuery)
                .ApplyFilters(filter);

            // Count BEFORE includes & pagination
            var totalRecords = await helper.Build().CountAsync();

            var query = helper
                .ApplyIncludes(includes)
                .ApplyOrdering(orderKey, orderDescending)
                .Build()
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);

            var categories = await query.ToListAsync();

            return new PagedResponse<IEnumerable<Category>>(
                categories,
                currentPage,
                pageSize,
                totalRecords);
        }

        public async Task<Category> GetCategoryByIdAsync(int id, CategoryIncludes includes)
        {
            var query = _context.Categories.AsQueryable();

            var category = await new CategoryQueryHelper(query)
                .ApplyIncludes(includes)
                .Build()
                .FirstOrDefaultAsync(c => c.Id == id);

            return category;
        }
    }
}
