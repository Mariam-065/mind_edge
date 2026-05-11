using Microsoft.EntityFrameworkCore;
using MindEdge_1.Data;
using MindEdge_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindEdge_1.Services
{
    public class StudyPlanService : IStudyPlanService
    {
        private readonly ApplicationDbContext _context;

        public StudyPlanService(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. توليد وحفظ في خطوة واحدة
        public async Task<StudyPlan> GenerateAndSavePlanAsync(StudyPlanResponseDto aiResponse)
        {
            var newPlan = new StudyPlan
            {
                FileName = aiResponse.Filename,
                CreatedAt = DateTime.Now,
                Days = aiResponse.StudyPlan.Select(d => new StudyDay
                {
                    DayNumber = d.Day,
                    Topic = d.Topic,
                    Tasks = d.Tasks.Select(t => new StudyTask
                    {
                        TaskName = t.TaskName,
                        Duration = t.Duration,
                        Priority = t.Priority,
                        IsCompleted = false
                    }).ToList()
                }).ToList()
            };

            _context.StudyPlans.Add(newPlan);
            await _context.SaveChangesAsync();
            return newPlan;
        }

        public async Task<List<object>> GetAllDashboardTasksAsync()
        {
            return await _context.StudyTasks
                .Where(t => t.IsCompleted == false)
                .Include(t => t.StudyDay)
                .ThenInclude(d => d.StudyPlan)
                .OrderByDescending(t => t.StudyDay.StudyPlan.CreatedAt)
                .Select(t => new {
                    t.Id,
                    t.TaskName,
                    t.Priority,
                    t.Duration,
                    FileName = t.StudyDay.StudyPlan.FileName,
                    TopicName = t.StudyDay.Topic
                })
                .ToListAsync<object>();
        }

        public async Task<bool> ToggleTaskAsync(int taskId)
        {
            var task = await _context.StudyTasks.FindAsync(taskId);
            if (task == null) return false;

            task.IsCompleted = !task.IsCompleted;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<object>> GetSavedPlansNamesAsync()
        {
            return await _context.StudyPlans
                .OrderByDescending(p => p.CreatedAt) // الأحدث يظهر الأول
                .Select(p => new {
                    p.Id,
                    p.FileName,
                    p.CreatedAt
                })
                .ToListAsync<object>();
        }
        public async Task<StudyPlan?> GetPlanByFileNameAsync(string fileName)
        {
            return await _context.StudyPlans
                .Include(p => p.Days)
                    .ThenInclude(d => d.Tasks)
                .FirstOrDefaultAsync(p => p.FileName == fileName);
        }
    }
}








