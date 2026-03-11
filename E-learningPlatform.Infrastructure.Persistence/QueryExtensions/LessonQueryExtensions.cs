using E_learningPlatform.Application.Features.Lessons.Queries.GetAllQuery;
using E_learningPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.QueryExtensions
{
    public static class LessonQueryExtensions
    {
        public static IQueryable<Lesson> ApplyFilters(this IQueryable<Lesson> query, LessonFilter? filter)
        {
            if (filter == null) return query;

            if (filter.Id.HasValue)
                query = query.Where(l => l.Id == filter.Id.Value);

            if (filter.SectionId.HasValue)
                query = query.Where(l => l.SectionId == filter.SectionId.Value);

            if (filter.IsPublished.HasValue)
                query = query.Where(l => l.IsPublished == filter.IsPublished.Value);

            if (!string.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(l => l.Title.ToLower().Contains(filter.Title.ToLower()));

            return query;
        }

        public static IQueryable<Lesson> ApplyIncludes(this IQueryable<Lesson> query, LessonIncludes? includes)
        {
            if (includes == null) return query;

            if (includes.LessonContent)
                query = query.Include(l => l.LessonContents.OrderBy(l => l.DisplayOrder));
            

            if (includes.Section)
                query = query.Include(s => s.Section);

            return query;
        }

        public static IQueryable<Lesson> ApplyOrdering(this IQueryable<Lesson> query, LessonOrderKey orderKey, bool orderDescending)
        {
            return orderKey switch
            {
                LessonOrderKey.Title => orderDescending ? query.OrderByDescending(l => l.Title) : query.OrderBy(l => l.Title),
                LessonOrderKey.DisplayOrder => orderDescending ? query.OrderByDescending(l => l.DisplayOrder) : query.OrderBy(l => l.DisplayOrder),
                LessonOrderKey.DurationMinutes => orderDescending ? query.OrderByDescending(l => l.DurationMinutes) : query.OrderBy(l => l.DurationMinutes),
                _ => query.OrderByDescending(l => l.CreatedAt)
            };
        }
    }
}
