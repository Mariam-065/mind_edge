<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
=======
﻿using System.ComponentModel.DataAnnotations;
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace MindEdge_1.Models
{

<<<<<<< HEAD
 public class GenerateQuizRequest
    {
        public string Filename { get; set; } = string.Empty;
        public int NumQuestions { get; set; } = 5;
=======
    public class GenerateQuizRequest
    {
        public string Filename { get; set; } = string.Empty;
        public int NumQuestions { get; set; } = 5;
        public string QuizType { get; set; }
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
    }

    public class SubmitQuizRequest
    {
        public string QuizId { get; set; } = string.Empty;
        public List<string> Answers { get; set; } = new();
    }




}