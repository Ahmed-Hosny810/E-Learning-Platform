using AutoMapper;
using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Features.UserProfiles.DTO;
using E_learningPlatform.Application.Features.UserProfiles.Queries.GetAllQuery;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.UserProfiles.Queries.GetByIdQuery
{
    public class GetUserProfileByIdQuery:IRequest<Response<UserProfileVm>>
    {
        public int Id { get; set; }
        public UserIncludes Includes { get; set; }
    }
    public class GetUserProfileByIdQueryHandler:IRequestHandler<GetUserProfileByIdQuery, Response<UserProfileVm>>
    {
        private readonly IUserProfileRepositoryAsync _userProfileRepository;
        private readonly IMapper _mapper;

        public GetUserProfileByIdQueryHandler(IUserProfileRepositoryAsync userProfileRepository,IMapper mapper)
        {
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
        }
        public async Task<Response<UserProfileVm>> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var userProfile = await  _userProfileRepository.GetUserProfileByIdAsync(request.Id, request.Includes);
            if (userProfile == null) throw new ApiException("User Profile not found");
            var userProfileVm = _mapper.Map<UserProfileVm>(userProfile);
            return new Response<UserProfileVm>(userProfileVm);
        }
    }
}
