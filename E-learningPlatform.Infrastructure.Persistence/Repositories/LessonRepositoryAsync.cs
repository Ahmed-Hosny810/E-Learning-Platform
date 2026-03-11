using E_learningPlatform.Application.Features.Lessons.Queries.GetAllQuery;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using E_learningPlatform.Infrastructure.Persistence.Contexts;
using E_learningPlatform.Infrastructure.Persistence.QueryExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.Repositories
{
    public class LessonRepositoryAsync : GenericRepositoryAsync<Lesson, int>, ILessonRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public LessonRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedResponse<IEnumerable<Lesson>>> GetLessonsPagedResponseAsync(LessonFilter filter, LessonIncludes includes, LessonOrderKey orderKey, bool orderDescending, int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var query = _context.Lessons.AsNoTracking();

            var totalRecords = await query.ApplyFilters(filter).CountAsync();

            var lessons = await query.ApplyFilters(filter)
                .ApplyIncludes(includes)
                .ApplyOrdering(orderKey, orderDescending)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<IEnumerable<Lesson>>(lessons, pageNumber, pageSize, totalRecords);
        }

        public async Task<Lesson> GetLessonByIdAsync(int id, LessonIncludes includes)
        {
            return await _context.Lessons
                .AsNoTracking()
                .ApplyIncludes(includes)
                .FirstOrDefaultAsync(l => l.Id == id);
        }
    }
}
