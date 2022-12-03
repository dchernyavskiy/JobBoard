using AutoMapper;
using JobBoard.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static JobBoard.Application.Locations.CreateLocation;
using static JobBoard.Application.Locations.DeleteLocation;
using static JobBoard.Application.Locations.GetLocations;

namespace JobBoard.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("api/v{apiVersion}/[controller]/[action]")]
    public class LocationController : BaseController
    {
        private readonly IMapper _mapper;

        public LocationController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<LocationsVm>> GetAll()
        {
            var query = new GetLocationsQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateLocationCommandDto createLocationCommandDto)
        {
            var command = _mapper.Map<CreateLocationCommand>(createLocationCommandDto);
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteLocationCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
