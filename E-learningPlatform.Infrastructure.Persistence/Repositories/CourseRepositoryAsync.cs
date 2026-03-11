using AutoMapper;
using E_learningPlatform.Application.Features.Courses.DTO;
using E_learningPlatform.Application.Features.Courses.Queries.GetAllCourses;
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
    public class CourseRepositoryAsync : GenericRepositoryAsync<Course, int>, ICourseRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public CourseRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedResponse<IEnumerable<Course>>> GetCoursesPagedResponseAsync(CourseFilter filter, CourseIncludes includes, CourseOrderKey orderKey, bool orderDescending, int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var query = _context.Courses.AsNoTracking();

            var totalRecords = await query.ApplyFilters(filter).CountAsync();

            var courses = await query
                  .ApplyFilters(filter)
                  .ApplyIncludes(includes)
                  .ApplyOrdering(orderKey, orderDescending)
                   .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                     .ToListAsync();

            return new PagedResponse<IEnumerable<Course>>(
                courses,
                pageNumber,
                pageSize,
                totalRecords);
        }

        public async Task<Course> GetCourseByIdAsync(int id, CourseIncludes includes)
        {
            var query = _context.Courses.AsQueryable();

            return await query.ApplyIncludes(includes)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}