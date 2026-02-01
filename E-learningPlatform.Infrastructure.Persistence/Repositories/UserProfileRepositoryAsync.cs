using E_learningPlatform.Application.Features.UserProfiles.Queries.GetAllQuery;
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
    public class UserProfileRepositoryAsync:GenericRepositoryAsync<UserProfile,int>, IUserProfileRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public UserProfileRepositoryAsync(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<UserProfile> GetUserProfileByIdAsync(int id, UserIncludes includes)
        {
            var query = _context.UserProfiles.AsQueryable();
            var userProfile = await new UserProfileQueryHelper(query)
                .ApplyIncludes(includes)
                .Build()
                .FirstOrDefaultAsync(up=>up.Id==id);
            return userProfile;
        }

        public async Task<UserProfile> GetUserProfileByUserIdAsync(string userId, UserIncludes includes)
        {
            var query = _context.UserProfiles.AsQueryable();
            var userProfile = await new UserProfileQueryHelper(query)
                .ApplyIncludes(includes)
                .Build()
                .FirstOrDefaultAsync(up => up.UserId == userId);
            return userProfile;
        }

        public async Task<PagedResponse<IEnumerable<UserProfile>>> GetUsersProfilesPagedResponseAsync(UserFilter filter, UserIncludes includes, UserProfileOrderKey orderKey, bool orderDescending, int currentPage, int pageSize)
        {
            currentPage = currentPage <= 0 ? 1 : currentPage;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            var baseQuery = _context.UserProfiles.AsQueryable();
            var helper = new UserProfileQueryHelper(baseQuery)
                .ApplyFilters(filter);
            var totalRecords = await helper.Build().CountAsync();
            var query = helper
                .ApplyIncludes(includes)
                .ApplyOrdering(orderKey, orderDescending)
                .Build()
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);
            var UsersProfiles = await query.ToListAsync();
            return new PagedResponse<IEnumerable<UserProfile>>(UsersProfiles, currentPage, pageSize, totalRecords);
        }
    }
}
