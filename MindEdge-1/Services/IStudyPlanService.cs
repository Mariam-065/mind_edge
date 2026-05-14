<<<<<<< HEAD
using MindEdge_1.Models;
=======
﻿using MindEdge_1.Models;
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
using System.Threading.Tasks;

namespace MindEdge_1.Services
{
    public interface IStudyPlanService
    {
<<<<<<< HEAD
            Task<StudyPlan> GenerateAndSavePlanAsync(StudyPlanResponseDto aiResponse);
            Task<List<object>> GetAllDashboardTasksAsync();
            Task<bool> ToggleTaskAsync(int taskId);
            Task<List<object>> GetSavedPlansNamesAsync();
            Task<StudyPlan?> GetPlanByFileNameAsync(string fileName);
=======
        Task<StudyPlan> GenerateAndSavePlanAsync(StudyPlanResponseDto aiResponse);
        Task<List<object>> GetAllDashboardTasksAsync();
        Task<bool> ToggleTaskAsync(int taskId);
        Task<List<object>> GetSavedPlansNamesAsync();
        Task<StudyPlan?> GetPlanByFileNameAsync(string fileName);
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
    }
}