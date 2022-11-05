using AutoMapper;
using JobBoard.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using static JobBoard.Application.Educations.CreateEducation;
using static JobBoard.Application.Educations.DeleteEducation;
using static JobBoard.Application.Educations.UpdateEducation;

namespace JobBoard.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("api/v{apiVersion}/[controller]/[action]")]
    public class EducationController : BaseController
    {
        private readonly IMapper _mapper;

        public EducationController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateEducationCommandDto commandDto)
        {
            var command = _mapper.Map<CreateEducationCommand>(commandDto);
            command.EmployeeId = UserId;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteEducationCommand
            {
                Id = id,
                EmployeeId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateEducationCommandDto commandDto)
        {
            var command = _mapper.Map<UpdateEducationCommand>(commandDto);
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
