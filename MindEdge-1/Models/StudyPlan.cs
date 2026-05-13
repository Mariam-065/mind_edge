using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MindEdge_1.Models
{
    public class StudyPlan
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<StudyDay> Days { get; set; } = new();
    }
}