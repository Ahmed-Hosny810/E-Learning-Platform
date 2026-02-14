using E_learningPlatform.Application.Features.LessonContents.Queries.GetAllQuery;
using E_learningPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.QueryHelpers
{
    public class LessonContentQueryHelper
    {
        private  IQueryable<LessonContent> _query;

        public LessonContentQueryHelper(IQueryable<LessonContent> query)
        {
            _query = query;
        }
        public LessonContentQueryHelper ApplyFilters(LessonContentFilter filter)
        {
            if (filter == null)
                return this;

            if (filter.LessonId.HasValue)
                _query = _query.Where(lc => lc.LessonId == filter.LessonId.Value);

            if (!string.IsNullOrWhiteSpace(filter.ContentType))
            {
                var ContentTypeLower = filter.ContentType.ToLower();
                _query = _query.Where(lc => lc.ContentType.ToString().ToLower().Contains(ContentTypeLower));
            }

            return this;
        }

        public LessonContentQueryHelper ApplyIncludes(LessonContentIncludes includes)
        {
            if (includes == null)
                return this;

            if (includes.Lesson)
            {
                _query = _query.Include(lc => lc.Lesson);
            }

            return this;
        }

        public LessonContentQueryHelper ApplyOrdering(
            LessonContentOrderKey orderKey,
            bool orderDescending)
        {
            _query = orderKey switch
            {

                LessonContentOrderKey.Id => orderDescending
                    ? _query.OrderByDescending(lc => lc.Id)
                    : _query.OrderBy(lc => lc.Id),

                LessonContentOrderKey.DisplayOrder => orderDescending
                    ? _query.OrderByDescending(lc => lc.DisplayOrder)
                    : _query.OrderBy(lc => lc.DisplayOrder),

                _ => _query.OrderByDescending(c => c.CreatedAt)
            };

            return this;
        }

        public IQueryable<LessonContent> Build()
        {
            return _query.AsNoTracking();
        }
    }
}
