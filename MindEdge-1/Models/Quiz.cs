using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace MindEdge_1.Models
{

    public class GenerateQuizRequest
    {
        public string Filename { get; set; } = string.Empty;
        public int NumQuestions { get; set; } = 5;
        public string QuizType { get; set; }
    }

    public class SubmitQuizRequest
    {
        public string QuizId { get; set; } = string.Empty;
        public List<string> Answers { get; set; } = new();
    }




}