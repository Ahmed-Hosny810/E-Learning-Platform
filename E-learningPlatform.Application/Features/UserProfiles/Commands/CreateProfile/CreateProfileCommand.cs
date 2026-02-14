using AutoMapper;
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

namespace E_learningPlatform.Application.Features.UserProfiles.Commands.CreateProfile
{
    public class CreateProfileCommand:IRequest<Response<int>>
    {
        public string DisplayName { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
    public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, Response<int>>
    {
        private readonly IUserProfileRepositoryAsync _userProfileRepository;
        private readonly IMapper _mapper;

        public CreateProfileCommandHandler(IUserProfileRepositoryAsync  userProfileRepository,IMapper mapper)
        {
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            var userProfile = _mapper.Map<UserProfile>(request);
            //userProfile.UserId = !! //  get this from the authenticated user context
            await  _userProfileRepository.AddAsync(userProfile);
            return new Response<int>(userProfile.Id);

        }
    }
}
