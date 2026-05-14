<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
=======
﻿using System.ComponentModel.DataAnnotations;
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MindEdge_1.Models
{
    public class StudyDay
    {
        public int Id { get; set; }
<<<<<<< HEAD
        public int DayNumber { get; set; } 
        public string Topic { get; set; } = string.Empty; 
=======
        public int DayNumber { get; set; }
        public string Topic { get; set; } = string.Empty;
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee

        public List<StudyTask> Tasks { get; set; } = new();

        public int StudyPlanId { get; set; }
        [JsonIgnore]
        public StudyPlan? StudyPlan { get; set; }
    }
}