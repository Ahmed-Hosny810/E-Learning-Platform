using E_learningPlatform.Application.Features.UserProfiles.Queries.GetAllQuery;
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
    public class UserProfileRepositoryAsync:GenericRepositoryAsync<UserProfile,int>, IUserProfileRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public UserProfileRepositoryAsync(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<UserProfile> GetUserProfileByIdAsync(int id, UserIncludes includes)
        {
            return await _context.UserProfiles
                  .AsNoTracking()
                  .ApplyIncludes(includes)
                  .FirstOrDefaultAsync(up => up.Id == id);
        }

        public async Task<UserProfile> GetUserProfileByUserIdAsync(string userId, UserIncludes includes)
        {
            return await _context.UserProfiles
                 .AsNoTracking()
                 .ApplyIncludes(includes)
                 .FirstOrDefaultAsync(up => up.UserId == userId);
        }

        public async Task<PagedResponse<IEnumerable<UserProfile>>> GetUsersProfilesPagedResponseAsync(UserFilter filter, UserIncludes includes, UserProfileOrderKey orderKey, bool orderDescending, int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            var query = _context.UserProfiles.AsNoTracking();
            var totalRecords = await query.ApplyFilters(filter).CountAsync();
            var UsersProfiles = await query.ApplyFilters(filter)
                .ApplyIncludes(includes)
                .ApplyOrdering(orderKey, orderDescending)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PagedResponse<IEnumerable<UserProfile>>(UsersProfiles, pageNumber, pageSize, totalRecords);
        }
    }
}
