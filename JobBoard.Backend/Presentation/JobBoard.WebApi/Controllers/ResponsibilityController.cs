using JobBoard.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize(Roles = "Employer")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateResponsibilityCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        //[Authorize(Roles = "Employer")]
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
