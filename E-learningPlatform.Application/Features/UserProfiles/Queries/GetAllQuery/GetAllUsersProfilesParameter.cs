using E_learningPlatform.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.UserProfiles.Queries.GetAllQuery
{
    public class GetAllUsersProfilesParameter: RequestParameter<UserProfileOrderKey>
    {
        public UserFilter Filter { get; set; } = new();
        public UserIncludes Includes { get; set; } = UserIncludes.None;
    }

    public class UserFilter
    {
        public int? Id { get; set; }
        public string? DisplayName { get; set; }
        public string? UserId { get; set; }
    }

    [Flags]
    public enum UserIncludes
    {
        None = 0,
        Enrollments = 1,
    }
    public enum UserProfileOrderKey
    {
        Id,
        UserId,
        DisplayName,
        CreatedDate
    }
}
