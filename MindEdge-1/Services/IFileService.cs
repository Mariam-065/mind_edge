using MindEdge_1.Models;

public interface IFileService
{
    Task<string> UploadAsync(FileUploadDto model, string userId);
    Task<byte[]?> DownloadAsync(string filename, string userId);
    Task<List<FileResponseDto>> GetFilesAsync(string userId); 
}