using CuraLinkDemoProject.CuraLinkDemo.Domain.Entities;
using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CuraLinkDemoProject.CuraLinkDemo.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MedicationsController : ControllerBase
    {
        private readonly CuraLinkDbContext _context;

        public MedicationsController(CuraLinkDbContext context)
        {
            _context = context;
        }

        [HttpGet("resident/{residentId}")]
        public async Task<IActionResult> GetByResident(int residentId)
        {
            var meds = await _context.Medications
                .Where(m => m.ResidentId == residentId)
                .ToListAsync();
            return Ok(meds);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Medication medication)
        {
            _context.Medications.Add(medication);
            await _context.SaveChangesAsync();
            return Ok(medication);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var med = await _context.Medications.FindAsync(id);
            if (med == null) return NotFound();
            _context.Medications.Remove(med);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
