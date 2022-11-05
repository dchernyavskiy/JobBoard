using Microsoft.AspNetCore.Mvc;
using static JobBoard.Application.Qualifications.CreateQualification;
using static JobBoard.Application.Qualifications.DeleteQualification;

namespace JobBoard.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("api/v{apiVersion}/[controller]/[action]")]
    public class QualificationController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody]CreateQualificationCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteQualificationCommand
            {
                Id = id,
                EmployerId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
