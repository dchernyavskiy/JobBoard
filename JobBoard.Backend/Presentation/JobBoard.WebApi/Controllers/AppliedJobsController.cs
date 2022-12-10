using AutoMapper;
using JobBoard.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using static JobBoard.Application.JobEmployees.CreateJobEmployee;

namespace JobBoard.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("api/v{apiVersion}/[controller]/[action]")]
    public class AppliedJobsController : BaseController
    {
        private readonly IMapper _mapper;

        public AppliedJobsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateJobEmployeeCommandDto commandDto)
        {
            var command = new CreateJobEmployeeCommand
            {
                JobId = commandDto.JobId,
                EmployeeId = UserId
            };
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }
    }
}