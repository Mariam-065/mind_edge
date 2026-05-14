using Microsoft.AspNetCore.Hosting;
using MindEdge_1.Models;
using System.Security.Cryptography;
using System.Text;
<<<<<<< HEAD
=======
using Microsoft.Extensions.Configuration;
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee

namespace MindEdge_1.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
<<<<<<< HEAD

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
=======
        private readonly IConfiguration _configuration;

        public FileService(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
        }

        public async Task<string> UploadAsync(FileUploadDto model, string userId)
        {
            if (model.File == null || model.File.Length == 0)
                return string.Empty;

            var userFolder = GetUserUploadFolder(userId);

            if (!Directory.Exists(userFolder))
                Directory.CreateDirectory(userFolder);

<<<<<<< HEAD
            var safeFileName = Path.GetFileName(model.File.FileName);
            var filePath = Path.Combine(userFolder, safeFileName);
=======
            var fileExtension = Path.GetExtension(model.File.FileName);
            var newFileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(userFolder, newFileName);
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee

            using var stream = new FileStream(filePath, FileMode.Create);
            await model.File.CopyToAsync(stream);

<<<<<<< HEAD
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

=======
            return newFileName;
        }

        public async Task<List<FileResponseDto>> GetFilesAsync(string userId)
        {
            var userFolder = GetUserUploadFolder(userId);

            if (!Directory.Exists(userFolder))
                return new List<FileResponseDto>();

            var apiUrl = _configuration["BaseUrl"] ?? "";
            var safeUserId = HashUserId(userId);

            var files = Directory.GetFiles(userFolder)
                .Select(filePath => {
                    var fileName = Path.GetFileName(filePath);
                    return new FileResponseDto
                    {
                        FileName = fileName,
                        FileUrl = $"{apiUrl}/uploads/{safeUserId}/{fileName}"
                    };
                })
                .ToList();

            return files;
        }

        public async Task<byte[]?> DownloadAsync(string filename, string userId)
        {
            if (string.IsNullOrWhiteSpace(filename)) return null;

            var userFolder = GetUserUploadFolder(userId);
            var safeFileName = Path.GetFileName(filename);
            var filePath = Path.Combine(userFolder, safeFileName);

            if (!File.Exists(filePath)) return null;

            return await File.ReadAllBytesAsync(filePath);
        }

        private string GetUserUploadFolder(string userId)
        {
            var rootPath = _environment.WebRootPath;
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
            if (string.IsNullOrEmpty(rootPath))
                rootPath = _environment.ContentRootPath;

            var safeUserId = HashUserId(userId);
<<<<<<< HEAD

=======
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
            return Path.Combine(rootPath, "uploads", safeUserId);
        }

        private static string HashUserId(string userId)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(userId));
            return Convert.ToHexString(bytes).ToLower();
        }
    }
}