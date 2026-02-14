using E_learningPlatform.Application.Features.Lessons.Queries.GetAllQuery;
using E_learningPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.QueryHelpers
{
    public class LessonQueryHelper
    {
        private  IQueryable<Lesson> _query;

        public LessonQueryHelper(IQueryable<Lesson> query)
        {
            _query = query;
        }

        public LessonQueryHelper ApplyFilters(LessonFilter? filter)
        {
            if (filter == null)
                return this;
            if (filter.Id.HasValue)
                _query = _query.Where(l => l.Id == filter.Id.Value);
            if (filter.SectionId.HasValue)
                _query = _query.Where(l => l.SectionId == filter.SectionId.Value);

            if (filter.IsPublished.HasValue)
                _query = _query.Where(l => l.IsPublished == filter.IsPublished.Value);

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                var nameLower = filter.Title.ToLower();
                _query = _query.Where(l => l.Title.ToLower().Contains(nameLower));
            }
            return this;
        }


        public LessonQueryHelper ApplyIncludes(LessonIncludes includes)
        {
            if (includes == null) return this;

            //if (includes.LessonContent)
            //{
            //    _query = _query.Include(l => l.LessonContents.OrderBy(l => l.DisplayOrder));
            //}

            if (includes.Section)
            {
                _query = _query.Include(s => s.Section);
            }

            return this;
    }

        public LessonQueryHelper ApplyOrdering(LessonOrderKey orderKey, bool orderDescending)
        {
            _query = orderKey switch
            {
                LessonOrderKey.Id => orderDescending
                    ? _query.OrderByDescending(l => l.Id)
                    : _query.OrderBy(l => l.Id),
                LessonOrderKey.Title => orderDescending
                    ? _query.OrderByDescending(l => l.Title)
                    : _query.OrderBy(l => l.Title),
                LessonOrderKey.DisplayOrder => orderDescending
                    ? _query.OrderByDescending(l => l.DisplayOrder)
                    : _query.OrderBy(l => l.DisplayOrder),
                LessonOrderKey.DurationMinutes => orderDescending
                    ? _query.OrderByDescending(l => l.DurationMinutes)
                    : _query.OrderBy(l => l.DurationMinutes),
                LessonOrderKey.IsPublished => orderDescending
                    ? _query.OrderByDescending(l => l.IsPublished)
                    : _query.OrderBy(l => l.IsPublished),
                LessonOrderKey.CreatedAt => orderDescending
                    ? _query.OrderByDescending(l => l.CreatedAt)
                    : _query.OrderBy(l => l.CreatedAt),
                LessonOrderKey.UpdatedAt => orderDescending
                    ? _query.OrderByDescending(l => l.UpdatedAt)
                    : _query.OrderBy(l => l.UpdatedAt),
                _ => _query
            };
            return this;
        }

        public IQueryable<Lesson> Build()
        {
            return _query.AsNoTracking();
        }
    }
}
