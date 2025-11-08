using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;
using CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces;
using System.Text.Json;

namespace CuraLinkDemoProject.CuraLinkDemo.Application.Services
{
    public class LLMService : ILLMService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public LLMService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClient = httpClientFactory.CreateClient("OpenAI");
            _config = config;
        }

        public async Task<ReportAnalysisResult> ExtractReportDataAsync(string reportText)
        {
            var apiKey = _config["LLM:ApiKey"];

            Console.WriteLine($"Using API Key: {(string.IsNullOrEmpty(apiKey) ? "MISSING" : "Found")}");

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("OpenAI API key is not configured");
            }

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var payload = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
            new
            {
                role = "system",
                content = @"Du bist ein medizinischer Assistent. Analysiere den Pflegebericht und extrahiere strukturierte Daten.
                Gib JSON in folgendem Format zurück:
                {
                  ""mealSchedules"": [
                    {
                      ""mealType"": ""Frühstück"",
                      ""mealTime"": ""2025-01-16T08:00:00"",
                      ""comments"": ""Normal"",
                      ""mealName"": ""Breakfast""
                    }
                  ],
                  ""movements"": [
                    {
                      ""room"": ""Zimmer 101"",
                      ""object"": ""Bett"",
                      ""angle"": 45,
                      ""movementTime"": ""2025-01-16T14:00:00"",
                      ""notes"": ""Transfer ins Bett"",
                      ""staffId"": 1
                    }
                  ],
                  ""ausscheidungen"": [
                    {
                      ""abstand"": ""3h"",
                      ""menge"": ""200ml"",
                      ""konsistenz"": ""normal"",
                      ""time"": ""2025-01-16T14:00:00"",
                      ""staffId"": 1
                    }
                  ]
                }
                
                Wenn keine Daten für eine Kategorie vorhanden sind, gib ein leeres Array zurück. Erinnerst du dich an aktuelle Datum und Uhrzeit."
            },
            new { role = "user", content = reportText }
        },
                temperature = 0.3,
                response_format = new { type = "json_object" }
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("chat/completions", payload);
                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"OpenAI Response Status: {response.StatusCode}");
                Console.WriteLine($"Full OpenAI Response: {responseContent}");  // ✅ Log full response

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"OpenAI Error: {responseContent}");
                    throw new HttpRequestException($"OpenAI API call failed: {response.StatusCode} - {responseContent}");
                }

                // Parse the response with better error handling
                var result = JsonSerializer.Deserialize<OpenAiResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                Console.WriteLine($"Parsed result - Choices count: {result?.Choices?.Count ?? 0}");

                if (result?.Choices == null || result.Choices.Count == 0)
                {
                    throw new InvalidOperationException("OpenAI response has no choices");
                }

                var jsonContent = result.Choices[0]?.Message?.Content;

                Console.WriteLine($"Extracted content length: {jsonContent?.Length ?? 0}");
                Console.WriteLine($"Extracted content: {jsonContent ?? "NULL"}");

                if (string.IsNullOrEmpty(jsonContent))
                {
                    throw new InvalidOperationException("OpenAI returned empty content");
                }

                var analysisResult = JsonSerializer.Deserialize<ReportAnalysisResult>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                Console.WriteLine($"Analysis result - MealSchedules: {analysisResult?.MealSchedules?.Count ?? 0}");
                Console.WriteLine($"Analysis result - Movements: {analysisResult?.Movements?.Count ?? 0}");

                return analysisResult ?? new ReportAnalysisResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calling OpenAI: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public Task<string> TranscribeAudioAsync(Stream audioStream)
        {
            throw new NotImplementedException("Audio transcription not yet implemented");
        }
    }

    public class OpenAiResponse
    {
        public List<OpenAiChoice> Choices { get; set; }
    }

    public class OpenAiChoice
    {
        public OpenAiMessage Message { get; set; }
    }

    public class OpenAiMessage
    {
        public string Content { get; set; }
    }

    public class WhisperResponse
    {
        public string Text { get; set; }
    }
}