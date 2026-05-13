using MindEdge_1.Models;

namespace MindEdge_1.Services
{
    public interface IChatService
    {
        Task<ChatResponseDto?> ProcessChatMessageAsync(string question, string sessionId, bool tts, string userId);
        Task<List<ChatMessage>> GetChatHistoryAsync(string sessionId, string userId);
    }
}