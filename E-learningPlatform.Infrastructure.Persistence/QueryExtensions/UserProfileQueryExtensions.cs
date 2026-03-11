using E_learningPlatform.Application.Features.UserProfiles.Queries.GetAllQuery;
using E_learningPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.QueryExtensions
{

    public static class UserProfileQueryExtensions
    {
        public static IQueryable<UserProfile> ApplyFilters(this IQueryable<UserProfile> query, UserFilter? filter)
        {
            if (filter == null) return query;

            if (filter.Id.HasValue)
                query = query.Where(up => up.Id == filter.Id.Value);

            if (!string.IsNullOrWhiteSpace(filter.DisplayName))
                query = query.Where(up => up.DisplayName.ToLower().Contains(filter.DisplayName.ToLower()));

            return query;
        }

        public static IQueryable<UserProfile> ApplyIncludes(this IQueryable<UserProfile> query, UserIncludes? includes)
        {
            if (includes == null) return query;
            if (includes.Enrollments)
                query = query.Include(up => up.Enrollments);
            return query;
        }

        public static IQueryable<UserProfile> ApplyOrdering(this IQueryable<UserProfile> query, UserProfileOrderKey orderKey, bool orderDescending)
        {
            return orderKey switch
            {
                UserProfileOrderKey.DisplayName => orderDescending ? query.OrderByDescending(up => up.DisplayName) : query.OrderBy(up => up.DisplayName),
                UserProfileOrderKey.Id => orderDescending ? query.OrderByDescending(up => up.Id) : query.OrderBy(up => up.Id),
                _ => query.OrderByDescending(c => c.CreatedAt)
            };
        }
    }
}
