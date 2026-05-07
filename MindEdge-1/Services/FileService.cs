using MindEdge_1.Models;

namespace MindEdge_1.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        public FileService(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration; 
        }
        public async Task<string> UploadAsync(FileUploadDto model)
        {
            
            if (model.File != null && model.File.Length > 0)
            {
                String uploadPath = Path.Combine( "uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                    string fileExtension = Path.GetExtension(model.File.FileName);


                    string newFileName = Guid.NewGuid().ToString() + fileExtension;


                    string filepath = Path.Combine(uploadPath, newFileName);                using (var stream = new FileStream(filepath, FileMode.Create))
                                    {
                    await model.File.CopyToAsync(stream);
                }
                return newFileName;
            }
            return string.Empty;
        }
        public async Task <byte[]?> DownloadAsync(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return null;

            }
            string filePath = Path.Combine( "uploads", filename);
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
            string uploadPath = Path.Combine(baseDirectory,  "uploads");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
                return null;
            }
            var apiUrl = _configuration["BaseUrl"];
            var files = Directory.GetFiles(uploadPath).Select(filePath => apiUrl + "/uploads/" + Path.GetFileName(filePath)).ToList();
            
            return files;
        }

      
    }
}
