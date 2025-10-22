using CuraLinkDemoProject.CuraLinkDemo.Api.Models;
using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;
using CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces;
using CuraLinkDemoProject.CuraLinkDemo.Domain.Entities;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CuraLinkDemoProject.CuraLinkDemo.Application.Services
{
    public class LLMService : ILLMService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public LLMService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClient = httpClientFactory.CreateClient("OpenAI"); ;
            _config = config;
        }
        

        public async Task<ReportAnalysisResult> ExtractReportDataAsync(string reportText)
        {
            var apiKey = _config["LLM:ApiKey"];

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            var payload = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                new { role = "system", content = "Du bist medizinischer Assistant. Gibst du JSON immer wie folgt zurück:\r\n{\r\n  \"medications\": [...],\r\n  \"painObservations\": [...],\r\n  \"mealSchedules\": [...],\r\n  \"movements\": [\r\n    {\r\n      \"room\": \"string\",\r\n      \"object\": \"string\",\r\n      \"angle\": number,\r\n      \"movementTime\": \"yyyy-MM-ddTHH:mm:ss\",\r\n      \"notes\": \"string\"\r\n    }\r\n,\r\n  \"ausscheidungen\": [\r\n    {\r\n      \"abstand\": \"string\",\r\n      \"menge\": \"string\",\r\n      \"Konsistenz\": string,\r\n      \"time\": \"yyyy-MM-ddTHH:mm:ss\",\r\n      \"residentId\": \"number\",\r\n      \"staffId\": \"number\",\r\n   }\r\n  ]\r\n}." },
                new { role = "user", content = reportText }
            }
            };

            var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", payload);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<OpenAiResponse>();

            var json = result.Choices[0].Message.Content;

            return JsonSerializer.Deserialize<ReportAnalysisResult>(json)!;
        }
    }

    public class WhisperResponse
    {
        public string Text { get; set; }
    }
}
