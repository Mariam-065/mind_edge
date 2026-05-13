using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace MindEdge_1.Models
{
    public class StudyTask
    {
        public int Id { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;

        public int StudyDayId { get; set; }
        [JsonIgnore]
        public StudyDay? StudyDay { get; set; }
    }
}