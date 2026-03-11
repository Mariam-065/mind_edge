using MindEdge_1.Models;

namespace MindEdge_1.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<bool> UploadAsync(FileUploadDto model)
        {
            
            if (model.File != null && model.File.Length > 0)
            {
                String uploadPath = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string filepath = Path.Combine(uploadPath, model.File.FileName);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }
                return true;
            }
            return false;
        }
        public async Task <byte[]?> DownloadAsync(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return null;

            }
            string filePath = Path.Combine(_environment.WebRootPath, "uploads", filename);
            if (!System.IO.File.Exists(filePath))
            {
                return null;

            }
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return fileBytes;
        }
        public async Task<List<string>> GetFilesAsync()
        {
            string baseDirectory = Directory.GetCurrentDirectory();
            string uploadPath = Path.Combine(baseDirectory, "wwwroot", "uploads");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
                return null;
            }

            var files = Directory.GetFiles(uploadPath).Select(Path.GetFileName).ToList();
            
            return files;
        }

      
    }
}
