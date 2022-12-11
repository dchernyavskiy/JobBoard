using AutoMapper;
using JobBoard.Application.Interfaces;
using JobBoard.Application.Jobs;
using JobBoard.Domain;
using JobBoard.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static JobBoard.Application.Jobs.ApplyJob;
using static JobBoard.Application.Jobs.CreateJob;
using static JobBoard.Application.Jobs.DeleteJob;
using static JobBoard.Application.Jobs.GetAppliedJobs;
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

        [HttpGet("UGetAppliedJobs")]
        public async Task<ActionResult<ICollection<Job>>> UGetAppliedJobs(Guid UserId)
        {
            var query = new GetAppliedJobsQuery
            {
                EmployeeId = UserId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetAppliedJobs")]
        public async Task<ActionResult<AppliedJobsVm>> GetAppliedJobs()
        {
            var query = new GetAppliedJobsQuery
            {
                EmployeeId = UserId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("UApplyJob")]
        public async Task<ActionResult> UApplyJob(Guid jobId, Guid UserId)
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

        [HttpPost("UCreate")]
        public async Task<ActionResult<Guid>> UCreate([FromBody] CreateJobCommandDto commandDto, Guid UserId)
        {
            var command = _mapper.Map<CreateJobCommand>(commandDto);
            command.EmployerId = UserId;
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateJobCommandDto commandDto)
        {
            var command = _mapper.Map<CreateJobCommand>(commandDto);
            command.EmployerId = UserId;
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }

        [HttpDelete("UDelete")]
        public async Task<IActionResult> UDelete(Guid id, Guid UserId)
        {
            var command = new DeleteJobCommand
            {
                Id = id,
                EmployerId = UserId == Guid.Empty ? Guid.Parse("041343ea-0f3d-458b-9fb6-7bd6700d69e8") : UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
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

        [HttpPut("UUpdate")]
        public async Task<IActionResult> UUpdate([FromBody] UpdateJobCommandDto commandDto, Guid UserId)
        {
            var command = _mapper.Map<UpdateJobCommand>(commandDto);
            command.EmployerId = UserId;
            var rm = await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateJobCommandDto commandDto)
        {
            var command = _mapper.Map<UpdateJobCommand>(commandDto);
            command.EmployerId = UserId;
            var rm = await Mediator.Send(command);
            return NoContent();
        }
    }
}