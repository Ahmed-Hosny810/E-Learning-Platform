using E_learningPlatform.Application.Features.Categories.Queries.GetAllCategories;
using E_learningPlatform.Application.Features.UserProfiles.Queries.GetAllQuery;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Interfaces.Repositories
{
    public interface IUserProfileRepositoryAsync:IGenericRepositoryAsync<UserProfile,int>
    {
        Task<PagedResponse<IEnumerable<UserProfile>>> GetUsersProfilesPagedResponseAsync(UserFilter filter, UserIncludes includes,
            UserProfileOrderKey orderKey, bool orderDescending, int currentPage, int pageSize);

        Task<UserProfile> GetUserProfileByUserIdAsync(string userId, UserIncludes includes);
        Task<UserProfile> GetUserProfileByIdAsync(int id, UserIncludes includes);
    }
}
