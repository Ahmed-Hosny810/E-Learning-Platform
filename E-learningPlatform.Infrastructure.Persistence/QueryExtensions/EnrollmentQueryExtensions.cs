using E_learningPlatform.Application.Features.Enrollments.Queries.GetAllQuery;
using E_learningPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace E_learningPlatform.Infrastructure.Persistence.QueryExtensions
{
    public static class EnrollmentQueryExtensions
    {
        public static IQueryable<Enrollment> ApplyFilters(this IQueryable<Enrollment> query, EnrollmentFilter? filter)
        {
            if (filter == null) return query;

            if (!string.IsNullOrWhiteSpace(filter.UserId))
                query = query.Where(e => e.UserProfile.UserId == filter.UserId);

            if (filter.CourseId.HasValue)
                query = query.Where(e => e.CourseId == filter.CourseId.Value);

            if (filter.IsPaid.HasValue)
                query = query.Where(e => e.IsPaid == filter.IsPaid.Value);

            if (filter.IsActive.HasValue)
                query = query.Where(e => e.IsActive == filter.IsActive.Value);

            return query;
        }

        public static IQueryable<Enrollment> ApplyIncludes(this IQueryable<Enrollment> query, EnrollmentIncludes? includes)
        {
            if (includes == null) return query;

            if (includes.Course)
                query = query.Include(e => e.Course);

            if (includes.Payments)
                query = query.Include(e => e.Payments);

            return query;
        }

        public static IQueryable<Enrollment> ApplyOrdering(this IQueryable<Enrollment> query, EnrollmentOrderKey orderKey, bool orderDescending)
        {
            return orderKey switch
            {
                EnrollmentOrderKey.CourseId => orderDescending ? query.OrderByDescending(e => e.CourseId) : query.OrderBy(e => e.CourseId),
                EnrollmentOrderKey.EnrolledAt => orderDescending ? query.OrderByDescending(e => e.EnrolledAt) : query.OrderBy(e => e.EnrolledAt),
                EnrollmentOrderKey.CompletedAt => orderDescending ? query.OrderByDescending(e => e.CompletedAt) : query.OrderBy(e => e.CompletedAt),
                EnrollmentOrderKey.PurchasePrice => orderDescending ? query.OrderByDescending(e => e.PurchasePrice) : query.OrderBy(e => e.PurchasePrice),
                _ => orderDescending ? query.OrderByDescending(e => e.CreatedAt) : query.OrderBy(e => e.CreatedAt)
            };
        }
    }

}