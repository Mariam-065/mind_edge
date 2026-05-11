using MindEdge_1.Models;
using System.Threading.Tasks;

namespace MindEdge_1.Services
{
    public interface IStudyPlanService
    {
            Task<StudyPlan> GenerateAndSavePlanAsync(StudyPlanResponseDto aiResponse);
            Task<List<object>> GetAllDashboardTasksAsync();
            Task<bool> ToggleTaskAsync(int taskId);
            Task<List<object>> GetSavedPlansNamesAsync();
            Task<StudyPlan?> GetPlanByFileNameAsync(string fileName);
    }
}