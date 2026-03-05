using E_learningPlatform.Application.Features.Enrollments.Queries.GetAllQuery;
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
    public class EnrollmentRepositoryAsync : GenericRepositoryAsync<Enrollment, int>, IEnrollmentRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedResponse<IEnumerable<Enrollment>>> GetEnrollmentsPagedResponseAsync(EnrollmentFilter filter, EnrollmentIncludes includes, EnrollmentOrderKey orderKey, bool orderDescending, int pageNumber, int pageSize)
        {
            var helper = new EnrollmentQueryHelper(_context.Enrollments.AsQueryable())
                .ApplyFilters(filter)
                .ApplyIncludes(includes);

            // Get count before ordering/paging for efficiency
            var totalRecords = await helper.Build().CountAsync();

            var enrollments = await helper.ApplyOrdering(orderKey, orderDescending)
                .Build()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<IEnumerable<Enrollment>>(enrollments, pageNumber, pageSize, totalRecords);
        }
        public async Task<Enrollment> GetEnrollmentByIdAsync(int id, EnrollmentIncludes includes)
        {
            return await new EnrollmentQueryHelper(_context.Enrollments.AsQueryable())
                .ApplyIncludes(includes)
                .Build()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> IsUserEnrolled(string userId, int courseId)
        {
            return await _context.Enrollments
                .AnyAsync(e => e.UserId == userId && e.CourseId == courseId && e.IsActive);
        }
    }
}
