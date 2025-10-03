using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;
using CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces;
using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace CuraLinkDemoProject.CuraLinkDemo.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ResidentsController : ControllerBase
    {
        private readonly IResidentService _residentService;
        private readonly CuraLinkDbContext _context;

        public ResidentsController(IResidentService residentService, CuraLinkDbContext context)
        {
            _residentService = residentService;
            _context = context;
        }

        // GET: api/residents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResidentDto>>> GetAll()
        {
            var residents = await _residentService.GetAllAsync();
            return Ok(residents);
        }

        // GET: api/residents/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ResidentDto>> GetById(int id)
        {
            var resident = await _residentService.GetByIdAsync(id);
            if (resident == null)
                return NotFound();

            return Ok(new
            {
                resident.Id,
                resident.FullName,
                resident.RoomNumber,
                resident.PhotoUrl
            }); 
        }

        // POST: api/residents
        [HttpPost]
        public async Task<ActionResult<ResidentDto>> Create([FromBody] CreateResidentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _residentService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/residents/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateResidentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _residentService.UpdateAsync(id, dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // GET: api/residents/{id}/appointments
        [HttpGet("{id}/appointments")]
        public async Task<ActionResult<ResidentWithAppointmentsDto>> GetResidentWithAppointments(int id)
        {
            var resident = await _residentService.GetResidentWithAppointmentsAsync(id);
            if (resident == null)
                return NotFound();

            return Ok(resident);
        }

        // DELETE: api/residents/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _residentService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
