using E_learningPlatform.Application.Features.Categories.Queries.GetAllCategories;
using E_learningPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace E_learningPlatform.Infrastructure.Persistence.QueryExtensions
{
    public static class CategoryQueryExtensions
    {
      
        public static IQueryable<Category> ApplyFilters(this IQueryable<Category> query,CategoryFilter filter)
        {
            if (filter == null)
                return query;

            if (filter.Id.HasValue)
                query = query.Where(c => c.Id == filter.Id.Value);

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                var nameLower = filter.Name.ToLower();
                query = query.Where(c => c.Name.ToLower().Contains(nameLower));
            }

            return query;
        }

        public static IQueryable<Category> ApplyIncludes(this IQueryable<Category> query, CategoryIncludes includes)
        {
            if (includes == null)
                return query;

            // Include the Parent Category
            if (includes.ParentCategory)
            {
                query = query.Include(c => c.ParentCategory);
            }

            // Include Subcategories (Children)
            if (includes.Subcategories)
            {
                query = query.Include(c => c.SubCategories);
            }


            return query;
        }

        public static IQueryable<Category> ApplyOrdering(this IQueryable<Category> query,CategoryOrderKey orderKey,bool orderDescending)
        {
            return orderKey switch
            {
                CategoryOrderKey.Name => orderDescending
                    ? query.OrderByDescending(c => c.Name)
                    : query.OrderBy(c => c.Name),

                CategoryOrderKey.Id => orderDescending
                    ? query.OrderByDescending(c => c.Id)
                    : query.OrderBy(c => c.Id),

                _ => query.OrderByDescending(c => c.CreatedAt)
            };

        }

    }
}

