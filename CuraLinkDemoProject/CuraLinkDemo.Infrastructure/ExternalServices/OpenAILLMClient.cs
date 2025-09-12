using System.Text.Json;

namespace CuraLinkDemoProject.CuraLinkDemo.Infrastructure.ExternalServices
{
    public class OpenAILLMClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public OpenAILLMClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<string> GetCompletionAsync(string prompt)
        {
            var apiKey = _config["LLM:ApiKey"];
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            var request = new
            {
                model = _config["LLM:Model"],
                prompt = prompt,
                max_tokens = 200
            };

            var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/completions", request);
            var result = await response.Content.ReadFromJsonAsync<JsonElement>();

            return result.GetProperty("choices")[0].GetProperty("text").GetString() ?? "";
        }
    }
}
