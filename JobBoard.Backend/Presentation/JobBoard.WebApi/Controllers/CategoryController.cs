using Microsoft.AspNetCore.Mvc;
using static JobBoard.Application.Categories.CreateCategory;
using static JobBoard.Application.Categories.DeleteCategory;
using static JobBoard.Application.Categories.GetCategories;

namespace JobBoard.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("api/v{apiVersion}/[controller]/[action]")]
    public class CategoryController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<CategoriesVm>> GetAll()
        {
            var vm = await Mediator.Send(new GetCategorysQuery());
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCategoryCommand command)
        {
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteCategoryCommand
            {
                Id = id,
            });
            return NoContent();
        }
    }
}