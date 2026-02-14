using E_learningPlatform.Application.Features.Lessons.Queries.GetAllQuery;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using E_learningPlatform.Infrastructure.Persistence.Contexts;
using E_learningPlatform.Infrastructure.Persistence.QueryHelpers;
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
            var helper = new LessonQueryHelper(_context.Lessons.AsQueryable())
                .ApplyFilters(filter)
                .ApplyIncludes(includes);

            // Get count before ordering/paging for efficiency
            var totalRecords = await helper.Build().CountAsync();

            var lessons = await helper.ApplyOrdering(orderKey, orderDescending)
                .Build()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<IEnumerable<Lesson>>(lessons, pageNumber, pageSize, totalRecords);
        }

        public async Task<Lesson> GetLessonByIdAsync(int id, LessonIncludes includes)
        {
            return await new LessonQueryHelper(_context.Lessons.AsQueryable())
                .ApplyIncludes(includes)
                .Build()
                .FirstOrDefaultAsync(l => l.Id == id);
        }
    }
}
