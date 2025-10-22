using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;
using CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces;
using CuraLinkDemoProject.CuraLinkDemo.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;

namespace CuraLinkDemoProject.CuraLinkDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LLMController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly TranscribeAudio _transcribeAudio;

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

        // POST /api/reports/voice
        [HttpPost("voice")]
        public async Task<IActionResult> PostVoiceReport(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var result = await _transcribeAudio.TranscribeAudioAsync(stream);
            return Ok(result);
        }
    }

    public class TranscribeAudio
    {
        private readonly HttpClient _httpClient;
        public async Task<string> TranscribeAudioAsync(Stream audioStream)
        {
            using var content = new MultipartFormDataContent();

            var fileContent = new StreamContent(audioStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("audio/webm");
            content.Add(fileContent, "file", "report.webm");

            content.Add(new StringContent("whisper-1"), "model");

            var response = await _httpClient.PostAsync("audio/transcriptions", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<WhisperResponse>();
            return result?.Text ?? string.Empty;
        }

    }
}