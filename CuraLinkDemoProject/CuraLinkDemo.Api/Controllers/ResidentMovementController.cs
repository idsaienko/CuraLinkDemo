using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CuraLinkDemoProject.CuraLinkDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResidentMovementsController : ControllerBase
    {
        private readonly CuraLinkDbContext _context;

        public ResidentMovementsController(CuraLinkDbContext context)
        {
            _context = context;
        }

        [HttpGet("{residentId}")]
        public async Task<IActionResult> GetMovements(int residentId)
        {
            var moves = await _context.ResidentMovements
                .Where(m => m.ResidentId == residentId)
                .OrderByDescending(m => m.MovementTime)
                .ToListAsync();

            return Ok(moves);
        }
    }
}
