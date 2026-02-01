using E_learningPlatform.Application.Interfaces.Repositories;
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
    public class CourseCategoryRepositoryAsync : GenericRepositoryAsync<CourseCategory, int>, ICourseCategoryRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public CourseCategoryRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CourseCategory?> GetByIdsAsync(int courseId, int categoryId)
        {
            return await _context.CourseCategories
                .FirstOrDefaultAsync(cc => cc.CourseId == courseId && cc.CategoryId == categoryId);
        }
        public async Task<bool> ExistsAsync(int courseId, int categoryId)
        {
            return await _context.CourseCategories
                .AnyAsync(cc =>
                    cc.CourseId == courseId &&
                    cc.CategoryId == categoryId);
        }
    }
}
