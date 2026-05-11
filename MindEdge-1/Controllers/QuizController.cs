using Microsoft.AspNetCore.Mvc;
using MindEdge_1.Data;
using MindEdge_1.Models;
using System.Text.Json;

namespace MindEdge_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly ExternalApiClient _apiClient;

        public QuizController(ExternalApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateQuiz([FromBody] GenerateQuizRequest request)
        {
            var aiResponse = await _apiClient.GenerateQuizAsync(
                request.Filename, request.NumQuestions);
            return Ok(aiResponse);
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitQuiz([FromBody] SubmitQuizRequest request)
        {
        

             var aiResponse = await _apiClient.SubmitQuizAsync(
                request.QuizId, request.Answers);
            return Ok(aiResponse);

           
        }
    }

   
}