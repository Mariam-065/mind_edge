using Microsoft.AspNetCore.Hosting;
using MindEdge_1.Models;
using System.Security.Cryptography;
using System.Text;

namespace MindEdge_1.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadAsync(FileUploadDto model, string userId)
        {
            if (model.File == null || model.File.Length == 0)
                return string.Empty;

            var userFolder = GetUserUploadFolder(userId);

            if (!Directory.Exists(userFolder))
                Directory.CreateDirectory(userFolder);

            var safeFileName = Path.GetFileName(model.File.FileName);
            var filePath = Path.Combine(userFolder, safeFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await model.File.CopyToAsync(stream);

            return filePath;
        }

        public async Task<byte[]?> DownloadAsync(string filename, string userId)
        {
            if (string.IsNullOrWhiteSpace(filename))
                return null;

            var userFolder = GetUserUploadFolder(userId);
            var safeFileName = Path.GetFileName(filename);
            var filePath = Path.Combine(userFolder, safeFileName);

            if (!File.Exists(filePath))
                return null;

            return await File.ReadAllBytesAsync(filePath);
        }

        public Task<List<string>> GetFilesAsync(string userId)
        {
            var userFolder = GetUserUploadFolder(userId);

            if (!Directory.Exists(userFolder))
                return Task.FromResult(new List<string>());

            var files = Directory.GetFiles(userFolder)
                .Select(Path.GetFileName)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Select(name => name!)
                .ToList();

            return Task.FromResult(files);
        }

        private string GetUserUploadFolder(string userId)
        {
            var rootPath = _environment.WebRootPath;

            if (string.IsNullOrEmpty(rootPath))
                rootPath = _environment.ContentRootPath;

            var safeUserId = HashUserId(userId);

            return Path.Combine(rootPath, "uploads", safeUserId);
        }

        private static string HashUserId(string userId)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(userId));
            return Convert.ToHexString(bytes).ToLower();
        }
    }
}