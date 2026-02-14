using E_learningPlatform.Application.Features.Sections.Queries.GetAllQuery;
using E_learningPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.QueryHelpers
{
    public class SectionQueryHelper
    {
        private IQueryable<Section> _query;

        public SectionQueryHelper(IQueryable<Section> query)
        {
            _query = query;
        }

        public SectionQueryHelper ApplyFilters(SectionFilter? filter)
        {
            if (filter == null)
                return this;
            if (filter.Id.HasValue)
                _query = _query.Where(s => s.Id == filter.Id.Value);
            if (filter.CourseId.HasValue)
                _query = _query.Where(s => s.CourseId == filter.CourseId.Value);

            if (filter.IsPublished.HasValue)
                _query = _query.Where(s => s.IsPublished == filter.IsPublished.Value);

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                var nameLower = filter.Title.ToLower();
                _query = _query.Where(s => s.Title.ToLower().Contains(nameLower));
            }
            return this;
        }


        public SectionQueryHelper ApplyIncludes(SectionIncludes includes)
        {
            if (includes == null) return this;

            if (includes.Lessons)
            { 
                _query = _query.Include(s => s.Lessons.OrderBy(l => l.DisplayOrder));
            }

            if (includes.Course)
            {
                _query = _query.Include(s => s.Course);
            }

            return this;
        }

        public SectionQueryHelper ApplyOrdering(SectionOrderKey orderKey, bool orderDescending)
        {
            _query = orderKey switch
            {
                SectionOrderKey.Title => orderDescending
                    ? _query.OrderByDescending(s => s.Title)
                    : _query.OrderBy(s => s.Title),
                SectionOrderKey.DisplayOrder => orderDescending
                    ? _query.OrderByDescending(s => s.DisplayOrder)
                    : _query.OrderBy(s => s.DisplayOrder),
                SectionOrderKey.DurationMinutes => orderDescending
                    ? _query.OrderByDescending(s => s.DurationMinutes)
                    : _query.OrderBy(s => s.DurationMinutes),
                SectionOrderKey.IsPublished => orderDescending
                    ? _query.OrderByDescending(s => s.IsPublished)
                    : _query.OrderBy(s => s.IsPublished),
                SectionOrderKey.CreatedAt => orderDescending
                    ? _query.OrderByDescending(s => s.CreatedAt)
                    : _query.OrderBy(s => s.CreatedAt),
                SectionOrderKey.UpdatedAt => orderDescending
                    ? _query.OrderByDescending(s => s.UpdatedAt)
                    : _query.OrderBy(s => s.UpdatedAt),
                _ => _query
            };
            return this;
        }

        public IQueryable<Section> Build()
        {
            return _query.AsNoTracking();
        }
    }
}
