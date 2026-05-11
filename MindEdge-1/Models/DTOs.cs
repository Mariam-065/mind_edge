using System.Text.Json.Serialization;

namespace MindEdge_1.Models
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string NewPassword { get; set; }
    }
    public class ChatRequestDto
    {
        [JsonPropertyName("question")]
        public string question { get; set; }

        [JsonPropertyName("session_id")]
        public string session_id { get; set; } = "default";

        [JsonPropertyName("filename")] 
        public string filename { get; set; }

        [JsonPropertyName("tts")]
        public bool tts { get; set; }

        [JsonPropertyName("tts_source")]
        public string tts_source { get; set; } = "response";
    }
    public class ChatResponseDto   
    {
        public string answer { get; set; }
        public string audio_url { get; set; }
        public string session_id { get; set; }
    }
public class StudyPlanResponseDto
 {
     [JsonPropertyName("status")]
     public string Status { get; set; }

     [JsonPropertyName("filename")]
     public string Filename { get; set; }

     [JsonPropertyName("study_plan")]
     public List<StudyDayDto> StudyPlan { get; set; } = new();
 }
 public class AIRequest
 {
     public int days { get; set; }
     public int hours_per_day { get; set; }
     public string level { get; set; }
 }

 public class StudyDayDto
 {
     [JsonPropertyName("day")]
     public int Day { get; set; }

     [JsonPropertyName("topic")]
     public string Topic { get; set; }

     [JsonPropertyName("tasks")]
     public List<StudyTaskDto> Tasks { get; set; } = new();
 }

 public class StudyTaskDto
 {
     [JsonPropertyName("task_name")]
     public string TaskName { get; set; }

     [JsonPropertyName("duration")]
     public string Duration { get; set; }

     [JsonPropertyName("priority")]
     public string Priority { get; set; }
 }
}