using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;
using CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CuraLinkDemoProject.CuraLinkDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] ReportDto dto)
        {
            var result = await _reportService.ProcessReportAsync(dto);
            return Ok(result);
        }
    }
}
