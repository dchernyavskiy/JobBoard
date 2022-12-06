using Microsoft.AspNetCore.Mvc;
using static JobBoard.Application.JobEmployees.CreateJobEmployee;
using static JobBoard.Application.JobEmployees.GetJobEmployee;

namespace JobBoard.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("api/v{apiVersion}/[controller]/[action]")]
    public class JobEnployeeController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<JobEmployeeVm>> GetAll()
        {
            var vm = await Mediator.Send(new GetJobEmployeeQuery());
            return Ok(vm);
        }


        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateJobEmployeeCommand command)
        {
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }
    }
}