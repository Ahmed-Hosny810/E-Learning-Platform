using Asp.Versioning;
using E_learningPlatform.Application.Features.QuizAttempts.Commands.CreateCommand;
using E_learningPlatform.Application.Features.QuizAttempts.DTO;
using E_learningPlatform.Application.Features.QuizAttempts.Queries.GetAll;
using E_learningPlatform.Application.Features.QuizAttempts.Queries.GetById;
using E_learningPlatform.Application.Features.Quizzes.DTO;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_learningPlatform.WebApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class QuizAttemptsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuizAttemptsController(IMediator mediator) => _mediator = mediator;


        [HttpPost("GetResult")]
        public async Task<ActionResult<Response<QuizResultDto>>> GetQuizResult(int attemptId)
        {
            return Ok(await _mediator.Send(new GetQuizAttemptResultQuery { AttemptId = attemptId }));
        }

        [HttpPost("GetHistory")]
        public async Task<ActionResult<PagedResponse<IEnumerable<QuizHistoryDto>>>> GetStudentQuizHistory(GetStudentQuizHistoryParameter parameter)
        {
            return Ok(await _mediator.Send(new GetStudentQuizHistoryQuery { Parameter = parameter }));
        }


        [HttpPost("StartQuiz")]
        public async Task<ActionResult<Response<int>>> StartQuiz(StartQuizAttemptCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        [HttpPost("SubmitQuiz")]

        public async Task<ActionResult<Response<int>>> SubmitQuiz(SubmitQuizCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        [HttpPut("AbandonQuiz")]
        public async Task<ActionResult<Response<int>>> AbandonQuiz(int Id)
        {
            return Ok(await _mediator.Send(new AbandonQuizAttemptCommand { QuizAttemptId = Id }));
        }
    }
}
