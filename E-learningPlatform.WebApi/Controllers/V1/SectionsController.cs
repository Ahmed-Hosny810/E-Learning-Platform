using Asp.Versioning;
using E_learningPlatform.Application.Features.Modules.Commands.CreateCommand;
using E_learningPlatform.Application.Features.Modules.DTO;
using E_learningPlatform.Application.Features.Modules.Queries.GetByIdQuery;
using E_learningPlatform.Application.Features.Sections.Commands.DeleteCommand;
using E_learningPlatform.Application.Features.Sections.Commands.UpdateCommand;
using E_learningPlatform.Application.Features.Sections.DTO;
using E_learningPlatform.Application.Features.Sections.Queries.GetAllQuery;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_learningPlatform.WebApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SectionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SectionsController(IMediator mediator) =>  _mediator = mediator;


        [HttpPost("GetAllSections")]
        public async Task<ActionResult<PagedResponse<IEnumerable<SectionVm>>>> Get(GetAllSectionsQueryParameter parameter)
        {

            return Ok(await _mediator.Send(new GetAllSectionsQuery() { Parameter = parameter }));
        }


        [HttpPost("GetSectionById/{id:int}")]
        public async Task<ActionResult<Response<SectionDetailedVm>>> Get(int Id, SectionIncludes includes)
        {
            return Ok(await _mediator.Send(new GetSectionByIdQuery { Id = Id, Includes = includes }));
        }


        [HttpPost("Add")]
        public async Task<ActionResult<Response<int>>> Post(CreateSectionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        [HttpPut("Update/{id:int}")]

        public async Task<ActionResult<Response<int>>> Update(UpdateSectionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        [HttpDelete("Delete/{id:int}")]
        public async Task<ActionResult<Response<int>>> Delete(int Id)
        {
            return Ok(await _mediator.Send(new DeleteSectionCommand { Id = Id }));
        }
    }
}
