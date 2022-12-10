using Microsoft.AspNetCore.Mvc;
using static JobBoard.Application.Responsibilities.CreateResponsibility;
using static JobBoard.Application.Responsibilities.DeleteResponsibility;

namespace JobBoard.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("api/v{apiVersion}/[controller]/[action]")]
    public class ResponsibilityController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateResponsibilityCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteResponsibilityCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}