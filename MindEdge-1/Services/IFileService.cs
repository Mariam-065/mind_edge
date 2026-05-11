
using MindEdge_1.Models;

namespace MindEdge_1.Services
{
    public interface IFileService
    {
        Task<string> UploadAsync(FileUploadDto model, string userId);

        Task<byte[]?> DownloadAsync(string filename, string userId);

        Task<List<string>> GetFilesAsync(string userId);
    }
}