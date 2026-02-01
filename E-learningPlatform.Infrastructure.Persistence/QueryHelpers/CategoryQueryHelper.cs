using E_learningPlatform.Application.Features.Categories.Queries.GetAllCategories;
using E_learningPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.QueryHelpers
{
    public class CategoryQueryHelper
    {
        private IQueryable<Category> _query;

        public CategoryQueryHelper(IQueryable<Category> query)
        {
            _query = query;
        }

        public CategoryQueryHelper ApplyFilters(CategoryFilter filter)
        {
            if (filter == null)
                return this;

            if (filter.Id.HasValue)
                _query = _query.Where(c => c.Id == filter.Id.Value);

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                var nameLower = filter.Name.ToLower();
                _query = _query.Where(c => c.Name.ToLower().Contains(nameLower));
            }

            return this;
        }

        public CategoryQueryHelper ApplyIncludes(CategoryIncludes includes)
        {
            if (includes == CategoryIncludes.None)
                return this;

            if (includes.HasFlag(CategoryIncludes.Parent))
                _query = _query.Include(c => c.ParentCategory);

            if (includes.HasFlag(CategoryIncludes.Children))
                _query = _query.Include(c => c.SubCategories);

            if (includes.HasFlag(CategoryIncludes.Courses))
                _query = _query
                    .Include(c => c.CourseCategories)
                    .ThenInclude(cc => cc.Course);

            return this;
        }

        public CategoryQueryHelper ApplyOrdering(
            CategoryOrderKey orderKey,
            bool orderDescending)
        {
            _query = orderKey switch
            {
                CategoryOrderKey.Name => orderDescending
                    ? _query.OrderByDescending(c => c.Name)
                    : _query.OrderBy(c => c.Name),

                CategoryOrderKey.Id => orderDescending
                    ? _query.OrderByDescending(c => c.Id)
                    : _query.OrderBy(c => c.Id),

                _ => _query.OrderByDescending(c => c.CreatedAt)
            };

            return this;
        }

        public IQueryable<Category> Build()
        {
            return _query;
        }
    }
}

