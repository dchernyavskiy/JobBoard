using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobBoard.Application.Aministration.BanEmployee;

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
    }
}