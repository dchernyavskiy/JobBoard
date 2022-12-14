using JobBoard.Domain;
using JobBoard.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using static JobBoard.Application.Employees.GetEmployee;
using static JobBoard.Application.Employees.UpdateEmployee;

namespace JobBoard.WebApi.Controllers;

[ApiVersionNeutral]
[Route("api/v{apiVersion}/[controller]/[action]")]
public class EmployeeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<EmployeeVm>> UGet(Guid UserId)
    {
        var query = new GetEmployeeQuery
        {
            Id = UserId,
        };
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<EmployeeVm>> Get()
    {
        var query = new GetEmployeeQuery
        {
            Id = UserId,
        };
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateEmployeeCommandDto command)
    {
        var query = new UpdateEmployeeCommand
        {
            Id = UserId,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Phone = command.Phone,
            CVLink = command.CVLink,
        };
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}