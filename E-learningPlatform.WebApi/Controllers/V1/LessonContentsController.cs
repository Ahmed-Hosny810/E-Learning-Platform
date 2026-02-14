using Asp.Versioning;
using E_learningPlatform.Application.Features.LessonContents.Commands.CreateCommand;
using E_learningPlatform.Application.Features.LessonContents.Commands.DeleteCommand;
using E_learningPlatform.Application.Features.LessonContents.Queries.GetAllQuery;
using E_learningPlatform.Application.Features.LessonContents.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_learningPlatform.WebApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class LessonContentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LessonContentsController(IMediator mediator) => _mediator = mediator;

        [HttpPost("GetContentById/{id}")]
        public async Task<IActionResult> GetById(int id,LessonContentIncludes includes)
        {
            return Ok(await _mediator.Send(new GetLessonContentByIdQuery { Id = id, Includes = includes }));
        }

        [HttpPost("Add")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm]CreateLessonContentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteLessonContentCommand { Id = id }));
        }


    }
}
