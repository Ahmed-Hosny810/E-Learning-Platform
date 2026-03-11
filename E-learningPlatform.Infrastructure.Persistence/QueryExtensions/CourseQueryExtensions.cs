using E_learningPlatform.Application.Features.Categories.Queries.GetAllCategories;
using E_learningPlatform.Application.Features.Courses.Queries.GetAllCourses;
using E_learningPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.QueryExtensions
{
    public static class CourseQueryExtensions
    {
        public static IQueryable<Course> ApplyFilters(this IQueryable<Course> query, CourseFilter? filter)
        {
            if (filter == null) return query;

            if (filter.Id.HasValue)
                query = query.Where(c => c.Id == filter.Id.Value);

            if (!string.IsNullOrWhiteSpace(filter.TeacherId))
                query = query.Where(c => c.TeacherId == filter.TeacherId);

            if (!string.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(c => c.Title.ToLower().Contains(filter.Title.ToLower()));

            if (filter.Level.HasValue)
                query = query.Where(c => c.Level == filter.Level.Value);

            if (filter.MinPrice.HasValue)
                query = query.Where(c => c.PriceUSD >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(c => c.PriceUSD <= filter.MaxPrice.Value);

            if (filter.MinRating.HasValue)
                query = query.Where(c => c.AverageRating >= filter.MinRating.Value);

            if (filter.CategoryIds != null && filter.CategoryIds.Any())
            {
                query = query.Where(c => c.CourseCategories
                             .Any(cc => filter.CategoryIds.Contains(cc.CategoryId)));
            }

            return query;
        }

        public static IQueryable<Course> ApplyIncludes(this IQueryable<Course> query, CourseIncludes? includes)
        {
            if (includes == null) return query;

            if (includes.Categories)
            {
                query = query.Include(c => c.CourseCategories)
                             .ThenInclude(cc => cc.Category);
            }

            if (includes.Sections)
            {
                query = query.Include(c => c.Sections.OrderBy(s => s.DisplayOrder));
            }

            return query;
        }

        public static IQueryable<Course> ApplyOrdering(this IQueryable<Course> query, CourseOrderKey orderKey, bool orderDescending)
        {
            return orderKey switch
            {
                CourseOrderKey.Title => orderDescending ? query.OrderByDescending(c => c.Title) : query.OrderBy(c => c.Title),
                CourseOrderKey.Price => orderDescending ? query.OrderByDescending(c => c.PriceUSD) : query.OrderBy(c => c.PriceUSD),
                CourseOrderKey.Level => orderDescending ? query.OrderByDescending(c => c.Level) : query.OrderBy(c => c.Level),
                CourseOrderKey.AverageRating => orderDescending ? query.OrderByDescending(c => c.AverageRating) : query.OrderBy(c => c.AverageRating),
                CourseOrderKey.EnrollmentCount => orderDescending ? query.OrderByDescending(c => c.EnrollmentCount) : query.OrderBy(c => c.EnrollmentCount),
                _ => query.OrderByDescending(c => c.CreatedAt)
            };
        }

    }
}
