using E_learningPlatform.Application.Features.Enrollments.Queries.GetAllQuery;
using E_learningPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace E_learningPlatform.Infrastructure.Persistence.QueryHelpers
{
    public class EnrollmentQueryHelper
    {
        private IQueryable<Enrollment> _query;

        public EnrollmentQueryHelper(IQueryable<Enrollment> query)
        {
            _query = query;
        }

        public EnrollmentQueryHelper ApplyFilters(EnrollmentFilter filter)
        {
            if (filter == null)
                return this;

            if (!string.IsNullOrWhiteSpace(filter.UserId))
                _query = _query.Where(e => e.UserId == filter.UserId);

            if (filter.CourseId.HasValue)
                _query = _query.Where(e => e.CourseId == filter.CourseId.Value);

            if (filter.IsPaid.HasValue)
                _query = _query.Where(e => e.IsPaid == filter.IsPaid.Value);

            if (filter.IsActive.HasValue)
                _query = _query.Where(e => e.IsActive == filter.IsActive.Value);

            return this;
        }

        public EnrollmentQueryHelper ApplyIncludes(EnrollmentIncludes includes)
        {
            if (includes == null)
                return this;

            if (includes.Course)
            {
                _query = _query.Include(e => e.Course);
            }

            if (includes.Payments)
            {
                _query = _query.Include(e => e.Payments);
            }

            return this;
        }

        public EnrollmentQueryHelper ApplyOrdering(
            EnrollmentOrderKey orderKey,
            bool orderDescending)
        {
            _query = orderKey switch
            {
                EnrollmentOrderKey.CourseId => orderDescending
                    ? _query.OrderByDescending(e => e.CourseId)
                    : _query.OrderBy(e => e.CourseId),

                EnrollmentOrderKey.EnrolledAt => orderDescending
                    ? _query.OrderByDescending(e => e.EnrolledAt)
                    : _query.OrderBy(e => e.EnrolledAt),

                EnrollmentOrderKey.CompletedAt => orderDescending
                    ? _query.OrderByDescending(e => e.CompletedAt)
                    : _query.OrderBy(e => e.CompletedAt),

                EnrollmentOrderKey.PurchasePrice => orderDescending
                    ? _query.OrderByDescending(e => e.PurchasePrice)
                    : _query.OrderBy(e => e.PurchasePrice),

                _ => orderDescending
                    ? _query.OrderByDescending(e => e.CreatedAt)
                    : _query.OrderBy(e => e.CreatedAt)
            };

            return this;
        }

        public IQueryable<Enrollment> Build()
        {
            
            return _query.AsNoTracking();
        }
    }
}