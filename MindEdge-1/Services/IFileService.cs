using MindEdge_1.Models;

namespace MindEdge_1.Services
{
    public interface IFileService
    {
        Task<string> UploadAsync(FileUploadDto model);
        Task<byte[]?> DownloadAsync(string filename);
        Task<List<string>> GetFilesAsync();


    }
}
