using CuraLinkDemoProject.CuraLinkDemo.Application.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly LLMService _llmService;

    public ReportsController(LLMService llmService)
    {
        _llmService = llmService;
    }

    // POST /api/reports/text
    [HttpPost("text")]
    public async Task<IActionResult> PostTextReport([FromBody] string report)
    {
        var result = await _llmService.AnalyzeReportAsync(report);
        return Ok(result);
    }

    // POST /api/reports/voice
    [HttpPost("voice")]
    public async Task<IActionResult> PostVoiceReport(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        var result = await _llmService.TranscribeAudioAsync(stream);
        return Ok(result);
    }
}
