using Microsoft.AspNetCore.Http;

namespace QuizLand.Application.Contract.DTOs.Upload;

public class UploadFileDto
{
    public IFormFile File { get; set; }
    public string? DirectoryName { get; set; }
    public string? FileName { get; set; }
    public string ExtensionFile { get; set; }
}