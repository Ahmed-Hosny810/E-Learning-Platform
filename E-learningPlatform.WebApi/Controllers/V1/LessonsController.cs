using Asp.Versioning;
using E_learningPlatform.Application.Features.Lessons.Commands.CreateCommand;
using E_learningPlatform.Application.Features.Lessons.Commands.DeleteCommand;
using E_learningPlatform.Application.Features.Lessons.Commands.UpdateCommand;
using E_learningPlatform.Application.Features.Lessons.DTO;
using E_learningPlatform.Application.Features.Lessons.Queries.GetAllQuery;
using E_learningPlatform.Application.Features.Lessons.Queries.GetByIdQuery;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_learningPlatform.WebApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class LessonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LessonsController(IMediator mediator) => _mediator = mediator;


        [HttpPost("GetAllLessons")]
        public async Task<ActionResult<PagedResponse<IEnumerable<LessonVm>>>> Get(GetAllLessonsQueryParameter parameter)
        {

            return Ok(await _mediator.Send(new GetAllLessonsQuery() { Parameter = parameter }));
        }


        [HttpPost("GetLessonById/{id:int}")]
        public async Task<ActionResult<Response<LessonVm>>> Get(int id, LessonIncludes includes)
        {
            return Ok(await _mediator.Send(new GetLessonByIdQuery { Id = id, Includes = includes }));
        }


        [HttpPost("Add")]
        public async Task<ActionResult<Response<int>>> Post(CreateLessonCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        [HttpPut("{id:int}")]

        public async Task<ActionResult<Response<int>>> Update(UpdateLessonCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Response<int>>> Delete(int Id)
        {
            return Ok(await _mediator.Send(new DeleteLessonCommand { Id = Id }));
        }
    }
}
