using JobBoard.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("api/v{apiVersion}/[controller]/[action]")]
    public class LocationController : BaseController
    {
        private readonly IJobBoardDbContext _context;

        public LocationController(IJobBoardDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Locations);
        }
    }
}
