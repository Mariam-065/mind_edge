using Microsoft.AspNetCore.Http;

namespace MindEdge_1.Models
{
    public class FileUploadDto
    {
        public IFormFile? File { get; set; }
    }
<<<<<<< HEAD
}
=======
    public class FileResponseDto
    {
        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
    }

}
>>>>>>> 1fa47f3bc21612e4eb00a3627a06e188035cf1ee
