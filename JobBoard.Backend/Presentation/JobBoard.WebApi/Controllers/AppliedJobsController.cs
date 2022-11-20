using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<Guid>> Create(CreateJobEmployeeCommand command)
        {
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }
    }
}
