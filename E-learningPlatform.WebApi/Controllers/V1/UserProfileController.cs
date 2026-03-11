using Asp.Versioning;
using E_learningPlatform.Application.Features.UserProfiles.Commands.CreateProfile;
using E_learningPlatform.Application.Features.UserProfiles.Commands.DeleteProfile;
using E_learningPlatform.Application.Features.UserProfiles.Commands.UpdateProfile;
using E_learningPlatform.Application.Features.UserProfiles.DTO;
using E_learningPlatform.Application.Features.UserProfiles.Queries.GetAllQuery;
using E_learningPlatform.Application.Features.UserProfiles.Queries.GetByIdQuery;
using E_learningPlatform.Application.Features.UserProfiles.Queries.GetByUserId;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_learningPlatform.WebApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UserProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // [HttpPost("GetAllUsersProfiles")]
        // public async Task<ActionResult<PagedResponse<IEnumerable<UserProfileVm>>>> Get(GetAllUsersProfilesParameter filter)
        // {

        //     return Ok(await _mediator.Send(new GetAllUsersProfilesQuery() { Parameter = filter }));
        // }


        [HttpPost("GetUserProfileById/{id:int}")]
        public async Task<ActionResult<Response<UserProfileVm>>> Get(int id, UserIncludes includes)
        {
            return Ok(await _mediator.Send(new GetUserProfileByIdQuery { Id = id, Includes = includes }));
        }

        [HttpPost("GetUserProfileByUserId/{UserId}")]
        public async Task<ActionResult<Response<UserProfileVm>>> Get(string userId, UserIncludes includes)
        {
            return Ok(await _mediator.Send(new GetUserProfileByUserIdQuery { UserId = userId, Includes = includes }));
        }


        [HttpPost("Add")]
        public async Task<ActionResult<Response<int>>> Post(CreateProfileCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        [HttpPost("Update/{id:int}")]

        public async Task<ActionResult<Response<int>>> Update(UpdateProfileCommand command)
        {

            return Ok(await _mediator.Send(command));
        }


        [HttpDelete("Delete/{id:int}")]
        public async Task<ActionResult<Response<int>>> Delete(int Id)
        {
            return Ok(await _mediator.Send(new DeleteProfileByIdCommand { Id = Id }));
        }
    }
}
