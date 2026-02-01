using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Features.Courses.DTO;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.UserProfiles.Commands.UpdateProfile
{
    public class UpdateProfileCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Response<int>>
    {
        private readonly IUserProfileRepositoryAsync _userProfileRepository;

        public UpdateProfileCommandHandler(IUserProfileRepositoryAsync userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }
        public async Task<Response<int>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = await _userProfileRepository.GetByIdAsync(request.Id);
            if (profile == null)
            {
                throw new ApiException($"Profile Not Found.");
            }
            profile.DisplayName=request.DisplayName;
            profile.Bio=request.Bio;
            profile.ProfilePictureUrl=request.ProfilePictureUrl;
            profile.UpdatedAt = DateTime.UtcNow;
            await _userProfileRepository.UpdateAsync(profile);
            return new Response<int>(profile.Id);
        }
    }
}
