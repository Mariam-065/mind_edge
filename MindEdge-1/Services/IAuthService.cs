using MindEdge_1.Models;

namespace MindEdge_1.Services
{
    public interface IAuthService
    {
        Task<bool> Register(User user);
        Task<string> Login(string email, string password);
    }
}