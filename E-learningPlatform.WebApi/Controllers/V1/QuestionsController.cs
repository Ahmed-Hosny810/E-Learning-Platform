using Asp.Versioning;
using E_learningPlatform.Application.Features.Questions.Commands;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_learningPlatform.WebApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionsController(IMediator mediator) => _mediator = mediator;

        [HttpPost("Add")]
        public async Task<ActionResult<Response<int>>> Add(AddQuestionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("Update/{id:int}")]
        public async Task<ActionResult<Response<int>>> Update(int id, UpdateQuestionCommand command)
        {
            if (id != command.Id) return BadRequest("Question ID mismatch.");
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<ActionResult<Response<int>>> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteQuestionCommand { Id = id }));
        }
    }
}
