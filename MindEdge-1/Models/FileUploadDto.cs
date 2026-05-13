namespace MindEdge_1.Models
{
    public class FileUploadDto
    {
        public IFormFile? File{ get; set; }
    }
    public class FileResponseDto
    {
        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
    }

}
