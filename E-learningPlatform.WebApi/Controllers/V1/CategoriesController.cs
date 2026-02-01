using Asp.Versioning;
using E_learningPlatform.Application.Features.Categories.Commands.CreateCategory;
using E_learningPlatform.Application.Features.Categories.Commands.UpdateCategory;
using E_learningPlatform.Application.Features.Categories.DTO;
using E_learningPlatform.Application.Features.Categories.Queries.GetAllCategories;
using E_learningPlatform.Application.Features.Categories.Queries.GetCategoryById;
using E_learningPlatform.Application.Features.Courses.Commands.DeleteCourse;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_learningPlatform.WebApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CategoriesController: ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator) => _mediator = mediator;

        [HttpPost("GetAllCategories")]
        public async Task<ActionResult<PagedResponse<IEnumerable<CategoryVm>>>> Get(GetAllCategoriesParameters filter)
        {

            return Ok(await _mediator.Send(new GetAllCategoriesQuery() { Parameter = filter }));
        }


        [HttpPost("GetCategoryById/{id:int}")]
        public async Task<ActionResult<Response<CategoryVm>>> Get(int Id, CategoryIncludes includes)
        {
            return Ok(await _mediator.Send(new GetCategoryByIdQuery { Id = Id, Includes = includes }));
        }


        [HttpPost("Add")]
        public async Task<ActionResult<Response<int>>> Post(CreateCategoryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        [HttpPost("Update/{id:int}")]

        public async Task<ActionResult<Response<int>>> Update(UpdateCategoryCommand command)
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
