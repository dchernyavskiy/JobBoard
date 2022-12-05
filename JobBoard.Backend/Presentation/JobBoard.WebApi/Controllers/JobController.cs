using AutoMapper;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using JobBoard.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static JobBoard.Application.Jobs.ApplyJob;
using static JobBoard.Application.Jobs.CreateJob;
using static JobBoard.Application.Jobs.DeleteJob;
using static JobBoard.Application.Jobs.GetJob;
using static JobBoard.Application.Jobs.GetJobs;
using static JobBoard.Application.Jobs.UpdateEducation;

namespace JobBoard.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("api/v{apiVersion}/[controller]/[action]")]
    public class JobController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IJobBoardDbContext _context;
        public JobController(IMapper mapper,
            IJobBoardDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("GetAppliedJobs")]
        public async Task<ActionResult<ICollection<Job>>> GetAppliedJobs()
        {
            var employee = await _context.Employees
                .Include(x => x.AppliedJobs)
                .FirstOrDefaultAsync(x => x.Id == UserId);

            var jobs = employee.AppliedJobs;

            return Ok(jobs);
        }

        [HttpPost]
        public async Task<ActionResult> ApplyJob(Guid jobId)
        {
            var command = new ApplyJobCommand
            {
                EmployeeId = UserId,
                JobId = jobId
            };
            var result = await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<JobsVm>> GetAll(GetJobsQuery query)
        {
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobVm>> Get(Guid id)
        {
            var query = new GetJobQuery
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        //[Authorize(Roles = "Employer")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateJobCommandDto commandDto)
        {
            var command = _mapper.Map<CreateJobCommand>(commandDto);
            command.EmployerId = UserId;
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }

        [HttpDelete]
        //[Authorize(Roles = "Employer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteJobCommand
            {
                Id = id,
                EmployerId = UserId == Guid.Empty ? Guid.Parse("041343ea-0f3d-458b-9fb6-7bd6700d69e8") : UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        //[Authorize(Roles = "Employer")]
        public async Task<IActionResult> Update([FromBody] UpdateJobCommandDto commandDto)
        {
            var command = _mapper.Map<UpdateJobCommand>(commandDto);
            command.EmployerId = UserId;
            var rm = await Mediator.Send(command);
            return NoContent();
        }
    }
}
