using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobBoard.Application.Aministration.BanEmployee;
using static JobBoard.Application.Aministration.BanEmployer;
using static JobBoard.Application.Aministration.GetEmployees;
using static JobBoard.Application.Aministration.GetEmployers;

namespace JobBoard.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Authorize(Roles = "SystemAdministrator")]
    [Route("api/v{apiVersion}/[controller]/[action]")]
    public class AdministratorController : BaseController
    {
        [HttpPut]
        public async Task<IActionResult> BanEmployee(Guid id)
        {
            var command = new BanEmployeeCommand
            {
                UserId = id,
            };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> BanEmployer(Guid id)
        {
            var command = new BanEmployerCommand
            {
                UserId = id,
            };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<AdminEmployeesVm>> GetEmployees()
        {
            var query = new GetEmployeesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<AdminEmployersVm>> GetEmployers()
        {
            var query = new GetEmployersQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}