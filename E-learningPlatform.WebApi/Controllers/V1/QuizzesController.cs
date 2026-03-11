using Asp.Versioning;
using E_learningPlatform.Application.Features.Quizzes.Commands.CreateCommand;
using E_learningPlatform.Application.Features.Quizzes.Commands.DeleteCommand;
using E_learningPlatform.Application.Features.Quizzes.Commands.UpdateCommand;
using E_learningPlatform.Application.Features.Quizzes.DTO;
using E_learningPlatform.Application.Features.Quizzes.Queries.GetByIdQuery;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_learningPlatform.WebApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class QuizzesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuizzesController(IMediator mediator) => _mediator = mediator;


        [HttpPost("GetQuizById/{id:int}")]
        public async Task<ActionResult<Response<QuizStudentDto>>> Get(int id)
        {
            return Ok(await _mediator.Send(new GetQuizForStudentQuery { QuizId = id }));
        }


        [HttpPost("Create")]
        public async Task<ActionResult<Response<int>>> Post(CreateQuizCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        [HttpPut("Update/{id:int}")]

        public async Task<ActionResult<Response<int>>> Update(UpdateQuizCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        [HttpDelete("Delete/{id:int}")]
        public async Task<ActionResult<Response<int>>> Delete(int Id)
        {
            return Ok(await _mediator.Send(new DeleteQuizCommand { Id = Id }));
        }
    }
}
