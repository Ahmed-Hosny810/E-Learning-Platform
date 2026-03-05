using AutoMapper;
using E_learningPlatform.Application.Features.Courses.DTO;
using E_learningPlatform.Application.Features.Courses.Queries.GetAllCourses;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using E_learningPlatform.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.Repositories
{
    public class CourseRepositoryAsync : GenericRepositoryAsync<Course, int>, ICourseRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public CourseRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedResponse<IEnumerable<Course>>> GetCoursesPagedResponseAsync(CourseFilter filter, CourseIncludes includes, CourseOrderKey orderKey, bool orderDescending, int currentPage, int pageSize)
        {
            currentPage = currentPage <= 0 ? 1 : currentPage;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var query = _context.Courses.AsQueryable();

            // Apply filters
            query = ApplyFilters(query, filter);

            // Get total count BEFORE pagination (important for performance)
            var totalRecords = await query.CountAsync();

            // Apply includes
            query = ApplyIncludes(query, includes);

            // Apply ordering
            query = ApplyOrdering(query, orderKey, orderDescending);

            // Apply pagination
            query = query.Skip((currentPage - 1) * pageSize).Take(pageSize);

            // Execute query 
            var courses = await query.ToListAsync();

            // Create paged response
            return new PagedResponse<IEnumerable<Course>>(
                courses,
                currentPage,
                pageSize,
                totalRecords);
        }

        public async Task<Course> GetCourseByIdAsync(int id, CourseIncludes includes)
        {
            var query = _context.Courses.AsQueryable();
            query = ApplyIncludes(query, includes);
            return await query.FirstOrDefaultAsync(c => c.Id == id);
        }



        #region Private Helper Methods

        private IQueryable<Course> ApplyFilters(IQueryable<Course> query, CourseFilter filter)
        {
            if (filter == null)
                return query;

            // ID filter
            if (filter.Id.HasValue)
                query = query.Where(c => c.Id == filter.Id.Value);

            // Teacher ID  to be removed
            if (!string.IsNullOrWhiteSpace(filter.TeacherId))
                query = query.Where(c => c.TeacherId == filter.TeacherId);

            // Title filter (exact match or contains)
            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                var titleLower = filter.Title.ToLower();
                query = query.Where(c => c.Title.ToLower().Contains(titleLower));
            }

            // Level filter
            if (filter.Level.HasValue)
                query = query.Where(c => c.Level == filter.Level.Value);

            // Language filter
            if (!string.IsNullOrWhiteSpace(filter.Language))
                query = query.Where(c => c.Language == filter.Language);

            // Price range filters
            if (filter.MinPrice.HasValue)
                query = query.Where(c => c.PriceUSD >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(c => c.PriceUSD <= filter.MaxPrice.Value);

            // Rating filter
            if (filter.MinRating.HasValue)
                query = query.Where(c => c.AverageRating >= filter.MinRating.Value);

            // Published date range filters
            if (filter.PublishedAfter.HasValue)
                query = query.Where(c => c.PublishedAt >= filter.PublishedAfter.Value);

            if (filter.PublishedBefore.HasValue)
                query = query.Where(c => c.PublishedAt <= filter.PublishedBefore.Value);

            // Category filter
            //if (filter.CategoryIds?.Any() == true)
            //{
            //    query = query.Where(c => c.CourseCategories
            //        .Any(cc => filter.CategoryIds.Contains(cc.CategoryId)));
            //}

            return query;
        }

        private IQueryable<Course> ApplyIncludes(IQueryable<Course> query, CourseIncludes includes)
        {
            if (includes == null)
                return query;

            // Include Categories
            if (includes.Categories)
            {
                query = query.Include(c => c.CourseCategories)
                             .ThenInclude(cc => cc.Category);
            }

            // Include Sections (Renamed from Modules)
            if (includes.Sections)
            {
                // Using DisplayOrder as discussed in your Section model
                query = query.Include(c => c.Sections.OrderBy(m => m.DisplayOrder));
            }

            //// Include Reviews
            //if (includes.Reviews)
            //{
            //    // Using filtered include to only bring back approved content
            //    query = query.Include(c => c.Reviews.Where(r => r.IsApproved));
            //}

            return query;
        }

        private IQueryable<Course> ApplyOrdering(
            IQueryable<Course> query,
            CourseOrderKey orderKey,
            bool orderDescending)
        {
            query = orderKey switch
            {
                CourseOrderKey.Title => orderDescending
                    ? query.OrderByDescending(c => c.Title)
                    : query.OrderBy(c => c.Title),

                CourseOrderKey.Price => orderDescending
                    ? query.OrderByDescending(c => c.PriceUSD)
                    : query.OrderBy(c => c.PriceUSD),

                CourseOrderKey.Language => orderDescending
                    ? query.OrderByDescending(c => c.Language)
                    : query.OrderBy(c => c.Language),

                CourseOrderKey.Level => orderDescending
                    ? query.OrderByDescending(c => c.Level)
                    : query.OrderBy(c => c.Level),

                CourseOrderKey.PublishedAt => orderDescending
                    ? query.OrderByDescending(c => c.PublishedAt)
                    : query.OrderBy(c => c.PublishedAt),

                CourseOrderKey.AverageRating => orderDescending
                    ? query.OrderByDescending(c => c.AverageRating)
                    : query.OrderBy(c => c.AverageRating),

                CourseOrderKey.EnrollmentCount => orderDescending
                    ? query.OrderByDescending(c => c.EnrollmentCount)
                    : query.OrderBy(c => c.EnrollmentCount),

                _ => query.OrderByDescending(c => c.CreatedAt) // Default
            };

            return query;
        }
  
        #endregion


    }
}