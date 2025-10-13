using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CuraLinkDemoProject.CuraLinkDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AusscheidungController : ControllerBase
    {
        private readonly CuraLinkDbContext _context;

        public AusscheidungController(CuraLinkDbContext context)
        {
            _context = context;
        }

        [HttpGet("resident/{residentId}")]
        public async Task<IActionResult> GetByResident(int residentId)
        {
            var ausscheidungen = await _context.Ausscheidungen
                .Where(a => a.ResidentId == residentId)
                .OrderByDescending(a => a.Time)
                .ToListAsync();

            return Ok(ausscheidungen);
        }
    }
}
