using E_learningPlatform.Application.Features.LessonContents.Queries.GetAllQuery;
using E_learningPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.QueryExtensions
{
    public static class LessonContentQueryExtensions
    {
        public static IQueryable<LessonContent> ApplyFilters(this IQueryable<LessonContent> query, LessonContentFilter? filter)
        {
            if (filter == null) return query;

            if (filter.LessonId.HasValue)
                query = query.Where(lc => lc.LessonId == filter.LessonId.Value);

            if (filter.ContentType.HasValue)
                query = query.Where(lc => lc.ContentType == filter.ContentType.Value);

            return query;
        }

        public static IQueryable<LessonContent> ApplyIncludes(this IQueryable<LessonContent> query, LessonContentIncludes? includes)
        {
            if (includes == null) return query;

            if (includes.Lesson)
                query = query.Include(lc => lc.Lesson);

            return query;
        }

        public static IQueryable<LessonContent> ApplyOrdering(this IQueryable<LessonContent> query, LessonContentOrderKey orderKey, bool orderDescending)
        {
            return orderKey switch
            {
                LessonContentOrderKey.Id => orderDescending ? query.OrderByDescending(lc => lc.Id) : query.OrderBy(lc => lc.Id),
                LessonContentOrderKey.DisplayOrder => orderDescending ? query.OrderByDescending(lc => lc.DisplayOrder) : query.OrderBy(lc => lc.DisplayOrder),
                _ => query.OrderByDescending(c => c.CreatedAt)
            };
        }
    }
}
