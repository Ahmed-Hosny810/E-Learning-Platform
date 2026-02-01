using Asp.Versioning;
using E_learningPlatform.Application.Features.Courses.Commands.CreateCourse;
using E_learningPlatform.Application.Features.Courses.Commands.DeleteCourse;
using E_learningPlatform.Application.Features.Courses.Commands.UpdateCourse;
using E_learningPlatform.Application.Features.Courses.DTO;
using E_learningPlatform.Application.Features.Courses.Queries.GetAllCourses;
using E_learningPlatform.Application.Features.Courses.Queries.GetCourseById;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_learningPlatform.WebApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)=> _mediator = mediator;

        [HttpPost("GetAllCourses")]
        public async Task<ActionResult<PagedResponse<IEnumerable<CourseVm>>>> Get(GetAllCoursesParameter filter)
        {

            return Ok(await _mediator.Send(new GetAllCoursesQuery() { Parameter = filter }));
        }

       
        [HttpPost("GetCourseById/{id:int}")]
        public async Task<ActionResult<Response<CourseVm>>> Get(int Id, CourseIncludes includes)
        {
            return Ok(await _mediator.Send(new GetCourseByIdQuery { Id = Id, Includes = includes }));
        }


        [HttpPost("Add")]
        public async Task<ActionResult<Response<int>>> Post(CreateCourseCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        [HttpPost("Update/{id:int}")]
        
        public async Task<ActionResult<Response<int>>> Update(UpdateCourseCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        [HttpDelete("Delete/{id:int}")]
       public async Task<ActionResult<Response<int>>> Delete(int Id)
        {
            return Ok(await _mediator.Send(new DeleteCategoryByIdCommand { Id = Id }));
        }

    }
}
