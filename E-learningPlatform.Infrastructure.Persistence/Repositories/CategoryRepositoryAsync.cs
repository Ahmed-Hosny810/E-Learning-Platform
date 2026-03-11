using E_learningPlatform.Application.Features.Categories.Queries.GetAllCategories;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using E_learningPlatform.Infrastructure.Persistence.Contexts;
using E_learningPlatform.Infrastructure.Persistence.QueryExtensions;
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
            int pageNumber,
            int pageSize)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var query = _context.Categories.AsNoTracking();
            var totalRecords=await query.ApplyFilters(filter).CountAsync();

            var categories = await query
                 .ApplyFilters(filter)
                 .ApplyIncludes(includes)
                 .ApplyOrdering(orderKey, orderDescending)
                  .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize)
                    .ToListAsync();

            return new PagedResponse<IEnumerable<Category>>(
                categories,
                pageNumber,
                pageSize,
                totalRecords);
        }

        public async Task<Category> GetCategoryByIdAsync(int id, CategoryIncludes includes)
        {
            var query = _context.Categories.AsQueryable();

            return await query.ApplyIncludes(includes)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
