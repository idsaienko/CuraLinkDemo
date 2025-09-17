using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;
using CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CuraLinkDemoProject.CuraLinkDemo.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LLMController : ControllerBase
    {
        private readonly IReportService _reportService;

        public LLMController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // POST api/llm/report
        [HttpPost("report")]
        public async Task<IActionResult> ProcessReport([FromBody] ReportDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.TextReport))
                return BadRequest("Report text is required.");

            var result = await _reportService.ProcessReportAsync(dto);
            return Ok(result);
        }
    }
}