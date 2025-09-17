using CuraLinkDemoProject.CuraLinkDemo.Api.Models;
using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;
using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CuraLinkDemoProject.CuraLinkDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealScheduleController : ControllerBase
    {
        private readonly CuraLinkDbContext _context;

        public MealScheduleController(CuraLinkDbContext context)
        {
            _context = context;
        }

        [HttpGet("resident/{residentId}")]
        public async Task<IActionResult> GetByResident(int residentId)
        {
            var meals = await _context.MealSchedules
                .Where(m => m.ResidentId == residentId)
                .OrderBy(m => m.MealTime)
                .Select(m => new MealScheduleDto
                {
                    Id = m.Id,
                    ResidentId = m.ResidentId,
                    MealType = m.MealType,
                    MealTime = m.MealTime,
                    DietaryNotes = m.DietaryNotes,
                    Menu = m.Menu
                })
                .ToListAsync();

            return Ok(meals);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MealScheduleDto dto)
        {
            var meal = new MealSchedule
            {
                ResidentId = dto.ResidentId,
                MealType = dto.MealType,
                MealTime = dto.MealTime,
                DietaryNotes = dto.DietaryNotes,
                Menu = dto.Menu
            };

            _context.MealSchedules.Add(meal);
            await _context.SaveChangesAsync();

            dto.Id = meal.Id;
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MealScheduleDto dto)
        {
            if (id != dto.Id) return BadRequest("Mismatched ID");

            var meal = await _context.MealSchedules.FindAsync(id);
            if (meal == null) return NotFound();

            meal.MealType = dto.MealType;
            meal.MealTime = dto.MealTime;
            meal.DietaryNotes = dto.DietaryNotes;
            meal.Menu = dto.Menu;

            await _context.SaveChangesAsync();
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var meal = await _context.MealSchedules.FindAsync(id);
            if (meal == null) return NotFound();

            _context.MealSchedules.Remove(meal);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
