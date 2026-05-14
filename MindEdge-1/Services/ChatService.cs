using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MindEdge_1.Data;
using MindEdge_1.Models;

namespace MindEdge_1.Services
{
    public class ChatService : IChatService
    {
        private readonly ApplicationDbContext _context;
        private readonly ExternalApiClient _externalApiClient;

        public ChatService(ApplicationDbContext context, ExternalApiClient externalApiClient)
        {
            _context = context;
            _externalApiClient = externalApiClient;
        }

        public async Task<ChatResponseDto?> ProcessChatMessageAsync(string question, string sessionId, bool tts, string userId)
        {
            var session = await _context.ChatbotRooms.FirstOrDefaultAsync(s => s.SessionId == sessionId);
            if (session == null) return null;

            _context.ChatMessages.Add(new ChatMessage { SessionId = sessionId, Role = "user", Content = question, UserId = userId });
            await _context.SaveChangesAsync();

            var chatRequest = new ChatRequestDto { question = question, filename = session.FileName, session_id = sessionId, tts = tts };
            var aiResponse = await _externalApiClient.SendChatMessageAsync(chatRequest);

            if (aiResponse != null)
            {
                _context.ChatMessages.Add(new ChatMessage { SessionId = sessionId, Role = "assistant", Content = aiResponse.answer, UserId = userId });
                await _context.SaveChangesAsync();
            }
            return aiResponse;
        }

        public async Task<List<ChatMessage>> GetChatHistoryAsync(string sessionId, string userId)
        {
            return await _context.ChatMessages
                .Where(m => m.SessionId == sessionId && m.UserId == userId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }
    }
}