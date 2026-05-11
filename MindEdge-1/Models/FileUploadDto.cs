using Microsoft.AspNetCore.Http;

namespace MindEdge_1.Models
{
    public class FileUploadDto
    {
        public IFormFile? File { get; set; }
    }
}