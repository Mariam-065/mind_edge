<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
=======
﻿using System.ComponentModel.DataAnnotations;
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MindEdge_1.Models
{
    public class StudyPlan
    {
        public int Id { get; set; }
<<<<<<< HEAD
        public string FileName { get; set; } = string.Empty; 
=======
        public string FileName { get; set; } = string.Empty;
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<StudyDay> Days { get; set; } = new();
    }
}