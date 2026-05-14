<<<<<<< HEAD
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
=======
using Azure;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee

namespace MindEdge_1.Models
{
    public class ExternalApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public ExternalApiClient(HttpClient httpClient, IWebHostEnvironment env, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _env = env;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["AIUrl"]??"");
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

        public async Task<SummaryResponse> GetSummaryAsync(string fileName, bool tts = false)
<<<<<<< HEAD
  {
      var response = await _httpClient.GetAsync($"summary?filename={Uri.EscapeDataString(fileName)}&tts={tts.ToString().ToLower()}");
      response.EnsureSuccessStatusCode();
=======
        {
            var response = await _httpClient.GetAsync($"summary?filename={Uri.EscapeDataString(fileName)}&tts={tts.ToString().ToLower()}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<SummaryResponse>(content);

            if (result != null && !string.IsNullOrEmpty(result.audio_url))
            {
                string baseUrl = _httpClient.BaseAddress.ToString().TrimEnd('/');
                result.audio_url = $"{baseUrl}{result.audio_url}";
            }

            return result; 
        }
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee

      var content = await response.Content.ReadAsStringAsync();

      var result = JsonSerializer.Deserialize<SummaryResponse>(content);

      if (result != null && !string.IsNullOrEmpty(result.audio_url))
      {
          string baseUrl = _httpClient.BaseAddress.ToString().TrimEnd('/');
          result.audio_url = $"{baseUrl}{result.audio_url}";
      }

      return result; 
  } 
        public async Task<string> GetDocumentDataAsync(string endpoint, string filename)
        {
            var response = await _httpClient.GetAsync($"{endpoint}?filename={Uri.EscapeDataString(filename)}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<ChatResponseDto> SendChatMessageAsync(ChatRequestDto chatRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("chat", chatRequest);

            if (!response.IsSuccessStatusCode)
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                throw new Exception($"AI Server Error: {response.StatusCode} - {errorMsg}");
            }

            return await response.Content.ReadFromJsonAsync<ChatResponseDto>();
        }
<<<<<<< HEAD

        public async Task<string> GenerateQuizAsync(string fileName, int numQuestions)
=======
        public async Task<StudyPlanResponseDto?> GetStudyPlanFromAIAsync(string fileName, object request)
        {
            var url = $"study-plan?filename={Uri.EscapeDataString(fileName)}&tts=false";

            var response = await _httpClient.PostAsJsonAsync(url, request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StudyPlanResponseDto>();
            }

            return null;
        }

        public async Task<string> GenerateQuizAsync(string fileName, int numQuestions, string quizType)
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
        {
            var requestBody = new
            {
                filename = fileName,
<<<<<<< HEAD
                num_questions = numQuestions
=======
                num_questions = numQuestions,
                quiz_type = quizType
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
<<<<<<< HEAD

=======
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
            var response = await _httpClient.PostAsync("quiz/generate", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
<<<<<<< HEAD
         public async Task<string> SubmitQuizAsync(string QuizId, List<string> Answers )
=======
         public async Task<string> SubmitQuizAsync(string QuizId, List<string> Answers)
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
        {
            var requestBody = new
            {
                quiz_id = QuizId,
                answers = Answers
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("quiz/submit", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
<<<<<<< HEAD
        public async Task<StudyPlanResponseDto?> GetStudyPlanFromAIAsync(string fileName, object request)
 {
     var url = $"study-plan?filename={Uri.EscapeDataString(fileName)}&tts=false";

     var response = await _httpClient.PostAsJsonAsync(url, request);

     if (response.IsSuccessStatusCode)
     {
         return await response.Content.ReadFromJsonAsync<StudyPlanResponseDto>();
     }

     return null;
 }
=======


>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
    }
}




