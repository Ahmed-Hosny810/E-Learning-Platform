using Asp.Versioning;
using E_learningPlatform.Application.Features.Enrollments.Commands.CreateCommand;
using E_learningPlatform.Application.Features.Enrollments.Commands.DeleteCommand;
using E_learningPlatform.Application.Features.Enrollments.Commands.UpdateCommand;
using E_learningPlatform.Application.Features.Enrollments.DTO;
using E_learningPlatform.Application.Features.Enrollments.Queries.GetAllQuery;
using E_learningPlatform.Application.Features.Enrollments.Queries.GetByIdQuery;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_learningPlatform.WebApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EnrollmentsController(IMediator mediator) => _mediator = mediator;

        [HttpPost("GetAllEnrollments")]
        public async Task<ActionResult<PagedResponse<IEnumerable<EnrollmentVm>>>> Get(GetAllEnrollmentsParameter filter)
        {

            return Ok(await _mediator.Send(new GetAllEnrollmentsQuery() { Parameter = filter }));
        }


        [HttpPost("GetEnrollmentById/{id:int}")]
        public async Task<ActionResult<Response<EnrollmentDetailedVm>>> Get(int Id, EnrollmentIncludes includes)
        {
            return Ok(await _mediator.Send(new GetEnrollmentByIdQuery { Id = Id, Includes = includes }));
        }


        [HttpPost("Add")]
        public async Task<ActionResult<Response<string>>> Post(CreateEnrollmentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        [HttpPost("Update/{id:int}")]

        public async Task<ActionResult<Response<int>>> UpdateEnrollmentProgress(UpdateEnrollmentProgressCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        [HttpDelete("Delete/{id:int}")]
        public async Task<ActionResult<Response<int>>> Delete(int Id)
        {
            return Ok(await _mediator.Send(new DeleteEnrollmentCommand { Id = Id }));
        }
    }
}
