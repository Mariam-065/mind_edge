using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MindEdge_1.Models
{
    public class ExternalApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _env;

        public ExternalApiClient(HttpClient httpClient, IWebHostEnvironment env)
        {
            _httpClient = httpClient;
            _env = env;
            
            _httpClient.BaseAddress = new Uri("https://instant-attraction-butler-indicating.trycloudflare.com");
            _httpClient.Timeout = TimeSpan.FromMinutes(5);

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> AnalyzeDocumentAsync(Stream fileStream, string fileName)
        {
            using var content = new MultipartFormDataContent();
            content.Add(new StreamContent(fileStream), "file", fileName);
           
            var response = await _httpClient.PostAsync("analyze-document", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetSummaryAsync(string fileName)
        {
            var response = await _httpClient.GetAsync($"summary?filename={Uri.EscapeDataString(fileName)}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetDocumentDataAsync(string endpoint, string filename)
        {
            var response = await _httpClient.GetAsync($"{endpoint}?filename={Uri.EscapeDataString(filename)}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<ChatResponseDto> SendChatMessageAsync(ChatRequestDto chatRequest)
        {
            // بنبعت الـ Dto اللي جاي لنا من الفلاتر زي ما هو للـ AI
            var response = await _httpClient.PostAsJsonAsync("chat", chatRequest);

            // بنعمل تشيك لو السيرفر رد بإيرور (زي 524 اللي شفناه قبل كدة)
            if (!response.IsSuccessStatusCode)
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                throw new Exception($"AI Server Error: {response.StatusCode} - {errorMsg}");
            }

            // بنحول الرد لـ Dto بتاعنا ونرجعه
            return await response.Content.ReadFromJsonAsync<ChatResponseDto>();
        }
    }
}