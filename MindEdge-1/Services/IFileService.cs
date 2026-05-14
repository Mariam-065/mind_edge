
using MindEdge_1.Models;

public interface IFileService
{
<<<<<<< HEAD
    public interface IFileService
    {
        Task<string> UploadAsync(FileUploadDto model, string userId);

        Task<byte[]?> DownloadAsync(string filename, string userId);

        Task<List<string>> GetFilesAsync(string userId);
    }
=======
    Task<string> UploadAsync(FileUploadDto model, string userId);
    Task<byte[]?> DownloadAsync(string filename, string userId);
    Task<List<FileResponseDto>> GetFilesAsync(string userId); 
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
}