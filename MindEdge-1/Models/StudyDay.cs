using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MindEdge_1.Models
{
    public class StudyDay
    {
        public int Id { get; set; }
        public int DayNumber { get; set; }
        public string Topic { get; set; } = string.Empty;

        public List<StudyTask> Tasks { get; set; } = new();

        public int StudyPlanId { get; set; }
        [JsonIgnore]
        public StudyPlan? StudyPlan { get; set; }
    }
}