<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
=======
﻿using System.ComponentModel.DataAnnotations;
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace MindEdge_1.Models
{
    public class StudyTask
    {
        public int Id { get; set; }
<<<<<<< HEAD
        public string TaskName { get; set; } = string.Empty; 
        public string Duration { get; set; } = string.Empty; 
        public string Priority { get; set; } = string.Empty; 
        public bool IsCompleted { get; set; } = false; 
=======
        public string TaskName { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee

        public int StudyDayId { get; set; }
        [JsonIgnore]
        public StudyDay? StudyDay { get; set; }
    }
}