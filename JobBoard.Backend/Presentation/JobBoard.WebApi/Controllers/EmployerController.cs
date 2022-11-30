using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateEmployerCommand commandDto)
        {
            var command = _mapper.Map<UpdateEmployerCommand>(commandDto);
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
