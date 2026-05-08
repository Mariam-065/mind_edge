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
}