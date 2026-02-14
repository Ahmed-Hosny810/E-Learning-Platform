using E_learningPlatform.Application.Features.LessonContents.Queries.GetAllQuery;
using E_learningPlatform.Application.Interfaces.Repositories;
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
    public class LessonContentRepositoryAsync : GenericRepositoryAsync<LessonContent, int>, ILessonContentRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public LessonContentRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<LessonContent> GetLessonContentByIdAsync(int id, LessonContentIncludes includes)
        {
            var query = _context.LessonContents.AsQueryable();

            var lessonContent = await new LessonContentQueryHelper(query)
                .ApplyIncludes(includes)
                .Build()
                .FirstOrDefaultAsync(c => c.Id == id);

            return lessonContent;
        }

        public async Task<int> GetMaxOrderByLessonId(int lessonId)
        {
            return await  _context.LessonContents
                .Where(lc=> lc.LessonId == lessonId)
                .Select(lc => (int?)lc.DisplayOrder)
                .MaxAsync() ?? 0;
        }
    }
}
