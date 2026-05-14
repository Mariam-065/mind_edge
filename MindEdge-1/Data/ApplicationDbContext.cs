using Microsoft.EntityFrameworkCore;
using MindEdge_1.Models;
using System.Collections.Generic;

namespace MindEdge_1.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<ChatbotRoom> ChatbotRooms { get; set; }
        public DbSet<AIResponse> AIResponses { get; set; }
        public DbSet<StudyPlan> StudyPlans { get; set; }
        public DbSet<StudyDay> StudyDays { get; set; }
        public DbSet<StudyTask> StudyTasks { get; set; }
<<<<<<< HEAD
=======
        public DbSet<ChatMessage> ChatMessages { get; set; }
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
    }
}