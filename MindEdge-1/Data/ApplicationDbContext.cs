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
    }
}