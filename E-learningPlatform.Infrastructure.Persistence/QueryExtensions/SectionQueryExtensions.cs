using E_learningPlatform.Application.Features.Sections.Queries.GetAllQuery;
using E_learningPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.QueryExtensions
{
    public static class SectionQueryExtensions
    {
        public static IQueryable<Section> ApplyFilters(this IQueryable<Section> query, SectionFilter? filter)
        {
            if (filter == null) return query;

            if (filter.Id.HasValue)
                query = query.Where(s => s.Id == filter.Id.Value);

            if (filter.CourseId.HasValue)
                query = query.Where(s => s.CourseId == filter.CourseId.Value);

            if (filter.IsPublished.HasValue)
                query = query.Where(s => s.IsPublished == filter.IsPublished.Value);

            if (!string.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(s => s.Title.ToLower().Contains(filter.Title.ToLower()));

            return query;
        }

        public static IQueryable<Section> ApplyIncludes(this IQueryable<Section> query, SectionIncludes? includes)
        {
            if (includes == null) return query;

            if (includes.Lessons)
                query = query.Include(s => s.Lessons.OrderBy(l => l.DisplayOrder));

            if (includes.Course)
                query = query.Include(s => s.Course);

            return query;
        }

        public static IQueryable<Section> ApplyOrdering(this IQueryable<Section> query, SectionOrderKey orderKey, bool orderDescending)
        {
            return orderKey switch
            {
                SectionOrderKey.Title => orderDescending ? query.OrderByDescending(s => s.Title) : query.OrderBy(s => s.Title),
                SectionOrderKey.DisplayOrder => orderDescending ? query.OrderByDescending(s => s.DisplayOrder) : query.OrderBy(s => s.DisplayOrder),
                SectionOrderKey.DurationMinutes => orderDescending ? query.OrderByDescending(s => s.DurationMinutes) : query.OrderBy(s => s.DurationMinutes),
                SectionOrderKey.IsPublished => orderDescending ? query.OrderByDescending(s => s.IsPublished) : query.OrderBy(s => s.IsPublished),
                _ => query.OrderByDescending(s => s.CreatedAt)
            };
        }
    }
}
