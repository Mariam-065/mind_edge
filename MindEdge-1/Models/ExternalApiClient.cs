using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json; 
using System.Threading.Tasks;
using System.IO;

namespace MindEdge_1.Models
{
    public class ExternalApiClient
    {
        private readonly HttpClient _httpClient;

        public ExternalApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://mindedgeai-production.up.railway.app/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> AnalyzeDocumentAsync(Stream fileStream, string fileName)
        {
            using var content = new MultipartFormDataContent();
            content.Add(new StreamContent(fileStream), "file", fileName);

            var response = await _httpClient.PostAsync("analyze-document", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetSummaryAsync(string filename)
        {
            var response = await _httpClient.GetAsync($"summary?filename={Uri.EscapeDataString(filename)}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetGraphsAsync()
        {
            var response = await _httpClient.GetAsync("graphs");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<ChatApiResponse> AskQuestionAsync(string question, string sessionId)
        {
            var requestBody = new { question = question, session_id = sessionId };

            var response = await _httpClient.PostAsJsonAsync("chat", requestBody);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ChatApiResponse>();
        }

        // Rules & Definitions
        public async Task<string> GetDocumentDataAsync(string endpoint, string filename)
        {
            var response = await _httpClient.GetAsync($"{endpoint}?filename={Uri.EscapeDataString(filename)}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }

    //ال DTO بيحل مشكلة ال Return Type
    public class ChatApiResponse
    {
        public string answer { get; set; }
        public string session_id { get; set; }
    }
}