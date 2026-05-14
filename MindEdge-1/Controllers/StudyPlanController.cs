<<<<<<< HEAD
=======
﻿using Microsoft.AspNetCore.Authorization;
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
using Microsoft.AspNetCore.Mvc;
using MindEdge_1.Data;
using MindEdge_1.Models;
using MindEdge_1.Services;
using System.Threading.Tasks;

namespace MindEdge_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
<<<<<<< HEAD
=======
    [Authorize]
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
    public class StudyPlanController : ControllerBase
    {
        private readonly IStudyPlanService _studyPlanService;
        private readonly ExternalApiClient _externalApiClient;

        public StudyPlanController(IStudyPlanService studyPlanService , ExternalApiClient externalApiClient)
        {
            _studyPlanService = studyPlanService;
            _externalApiClient = externalApiClient;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate(string filename, [FromBody] AIRequest input)
        {
            var aiResponse = await _externalApiClient.GetStudyPlanFromAIAsync(filename, input);

            if (aiResponse == null)
                return StatusCode(500, "AI Server did not respond correctly");

            var savedPlan = await _studyPlanService.GenerateAndSavePlanAsync(aiResponse);

            return Ok(savedPlan);
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            return Ok(await _studyPlanService.GetAllDashboardTasksAsync());
        }

        [HttpPatch("tasks/{id}/toggle")]
        public async Task<IActionResult> Toggle(int id)
        {
            var success = await _studyPlanService.ToggleTaskAsync(id);
            return success ? Ok(new { message = "Status Updated" }) : NotFound();
        }

        [HttpGet("archive-names")]
        public async Task<IActionResult> GetArchiveNames()
        {
            var plans = await _studyPlanService.GetSavedPlansNamesAsync();
            return Ok(plans);
        }
        [HttpGet("plan-by-file")]
        public async Task<IActionResult> GetPlanByFile(string fileName)
        {
            var plan = await _studyPlanService.GetPlanByFileNameAsync(fileName);

            if (plan == null)
                return NotFound(new { message = "No plan found for this file" });

            return Ok(plan);
        }
    }
}