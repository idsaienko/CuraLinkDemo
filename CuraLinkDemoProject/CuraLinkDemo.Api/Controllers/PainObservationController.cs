using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;
using CuraLinkDemoProject.CuraLinkDemo.Domain.Entities;
using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CuraLinkDemoProject.CuraLinkDemo.Api.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    public class PainObservationController : ControllerBase
    {
        private readonly CuraLinkDbContext _context;

        public PainObservationController(CuraLinkDbContext context)
        {
            _context = context;
        }

        // GET: api/painobservation/resident/5
        [HttpGet("resident/{residentId}")]
        public async Task<IActionResult> GetByResident(int residentId)
        {
            var observations = await _context.PainObservations
                .Where(p => p.ResidentId == residentId)
                .OrderBy(p => p.Time)
                .Select(p => new PainObservationDto
                {
                    Id = p.Id,
                    ResidentId = p.ResidentId,
                    Score = p.Score,
                    Location = p.Location,
                    Notes = p.Notes,
                    Time = p.Time
                })
                .ToListAsync();

            return Ok(observations);
        }

        // POST: api/painobservation
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PainObservationDto dto)
        {
            var observation = new PainObservation
            {
                ResidentId = dto.ResidentId,
                Score = dto.Score,
                Location = dto.Location,
                Notes = dto.Notes,
                Time = dto.Time
            };

            _context.PainObservations.Add(observation);
            await _context.SaveChangesAsync();

            dto.Id = observation.Id; // возвращаем Id новой записи
            return Ok(dto);
        }

        // PUT: api/painobservation/3
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PainObservationDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Mismatched ID");

            var obs = await _context.PainObservations.FindAsync(id);
            if (obs == null) return NotFound();

            obs.Score = dto.Score;
            obs.Location = dto.Location;
            obs.Notes = dto.Notes;
            obs.Time = dto.Time;

            await _context.SaveChangesAsync();
            return Ok(dto);
        }

        // DELETE: api/painobservation/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var obs = await _context.PainObservations.FindAsync(id);
            if (obs == null) return NotFound();

            _context.PainObservations.Remove(obs);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
