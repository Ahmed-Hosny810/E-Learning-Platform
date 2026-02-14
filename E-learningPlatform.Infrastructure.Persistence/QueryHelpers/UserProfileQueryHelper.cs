using E_learningPlatform.Application.Features.UserProfiles.Queries.GetAllQuery;
using E_learningPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.QueryHelpers
{
    public class UserProfileQueryHelper
    {
        private IQueryable<UserProfile> _query;

        public UserProfileQueryHelper(IQueryable<UserProfile> query)
        {
            _query = query;
        }

        public UserProfileQueryHelper ApplyFilters(UserFilter filter)
        {
            if (filter == null)
                return this;

            if (filter.Id.HasValue)
                _query = _query.Where(up => up.Id == filter.Id.Value);

            if (!string.IsNullOrWhiteSpace(filter.DisplayName))
            {
                var nameLower = filter.DisplayName.ToLower();
                _query = _query.Where(up => up.DisplayName.ToLower().Contains(nameLower));
            }

            return this;
        }

        public UserProfileQueryHelper ApplyIncludes(UserIncludes includes)
        {

            //if (includes.Enrollments)
            //    _query = _query.Include(u =>u.Enrollments);

            return this;
        }

        public UserProfileQueryHelper ApplyOrdering(
            UserProfileOrderKey orderKey,
            bool orderDescending)
        {
            _query = orderKey switch
            {
                UserProfileOrderKey.DisplayName => orderDescending
                    ? _query.OrderByDescending(up => up.DisplayName)
                    : _query.OrderBy(up => up.DisplayName),

                UserProfileOrderKey.Id => orderDescending
                    ? _query.OrderByDescending(up => up.Id)
                    : _query.OrderBy(up => up.Id),

                _ => _query.OrderByDescending(c => c.CreatedAt)
            };

            return this;
        }

        public IQueryable<UserProfile> Build()
        {
            return _query.AsNoTracking();
        }
    }
}
