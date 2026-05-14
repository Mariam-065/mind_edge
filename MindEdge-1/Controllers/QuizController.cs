<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc;
=======
﻿using Microsoft.AspNetCore.Mvc;
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
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
<<<<<<< HEAD
                request.Filename, request.NumQuestions);
=======
                request.Filename, request.NumQuestions, request.QuizType);
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
            return Ok(aiResponse);
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitQuiz([FromBody] SubmitQuizRequest request)
        {
<<<<<<< HEAD
        

             var aiResponse = await _apiClient.SubmitQuizAsync(
                request.QuizId, request.Answers);
            return Ok(aiResponse);

           
        }
    }

   
=======


            var aiResponse = await _apiClient.SubmitQuizAsync(
               request.QuizId, request.Answers);
            return Ok(aiResponse);


        }
    }


>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
}