using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using static JobBoard.Application.Employers.GetEmployer;
using static JobBoard.Application.Employers.GetEmployers;
using static JobBoard.Application.Employers.UpdateEmployer;

namespace JobBoard.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("api/v{apiVersion}/[controller]/[action]")]
    public class EmployerController : BaseController
    {
        private readonly IMapper _mapper;

        public EmployerController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployerVm>> Get(Guid id)
        {
            var query = new GetEmployerQuery
            {
                EmployerId = id
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<EmployersVm>> GetAll()
        {
            var query = new GetEmployersQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateEmployerCommand commandDto)
        {
            var command = _mapper.Map<UpdateEmployerCommand>(commandDto);
            await Mediator.Send(command);
            return NoContent();
        }
    }
}