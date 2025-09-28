using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;
using CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CuraLinkDemoProject.CuraLinkDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AusscheidungController : ControllerBase
    {
        private readonly IAusscheidungService _service;

        public AusscheidungController(IAusscheidungService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AusscheidungDto dto)
        {
            var result = await _service.AddAsync(dto);
            return Ok(result);
        }

        [HttpGet("{residentId}")]
        public async Task<IActionResult> GetByResident(int residentId)
        {
            var result = await _service.GetByResidentAsync(residentId);
            return Ok(result);
        }
    }
}
