using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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
