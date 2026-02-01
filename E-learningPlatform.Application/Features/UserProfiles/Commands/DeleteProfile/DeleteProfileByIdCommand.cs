using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.UserProfiles.Commands.DeleteProfile
{
    public class DeleteProfileByIdCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteProfileByIdCommandHandler : IRequestHandler<DeleteProfileByIdCommand, Response<int>>
    {
        private readonly IUserProfileRepositoryAsync _userProfileRepository;

        public DeleteProfileByIdCommandHandler(IUserProfileRepositoryAsync userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }
        public async Task<Response<int>> Handle(DeleteProfileByIdCommand request, CancellationToken cancellationToken)
        {
            var profile= await  _userProfileRepository.GetByIdAsync(request.Id);
            if (profile == null) throw new ApiException($"Profile Not Found.");
            await _userProfileRepository.DeleteAsync(profile);
            return new Response<int>(profile.Id);


        }
    }
}
